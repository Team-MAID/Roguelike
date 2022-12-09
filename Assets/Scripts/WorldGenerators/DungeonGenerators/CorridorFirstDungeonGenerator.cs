using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DungeonGeneration.RandomWalkGeneration;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace DungeonGeneration
{
    public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
    {
        [SerializeField] private int corridorLength = 14, corridorCount = 5;

        [SerializeField] [Range(0.1f, 1f)] private float roomPercent = .8f;

        protected override void RunProceduralGeneration()
        {
            CorridorFirstGeneration();
        }

        private void CorridorFirstGeneration()
        {
            var floorPositions = new HashSet<Vector2Int>();
            var potentialRoomPositions = new HashSet<Vector2Int>();

            // Create the corridor's floors
            CreateCorridors(floorPositions, potentialRoomPositions);

            // Create the room's floors at end of corridors
            var roomPositions = CreateRooms(potentialRoomPositions);

            // Create room's floors at dead end
            List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
            CreateRoomsAtDeadEnd(deadEnds, roomPositions);
            
            floorPositions.UnionWith(roomPositions);
            
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
        {
            foreach (var position in deadEnds)
            {
                if (!roomFloors.Contains(position))
                {
                    RunRandomWalk(randomWalkParameters, position);
                    roomFloors.UnionWith(FloorPositions);
                }
            }
        }

        private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
        {
            var deadEnds = new List<Vector2Int>();
            foreach (var floorPosition in floorPositions)
            {
                int neighboursCount = 0;
                foreach (var direction in Direction2D.CardinalDirections)
                {
                    if (floorPositions.Contains(floorPosition + direction))
                    {
                        neighboursCount++;
                    }
                    
                    if (neighboursCount == 1)
                        deadEnds.Add(floorPosition);
                }
            }

            return deadEnds;
        }

        private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
        {
            var currentPosition = startPosition;

            potentialRoomPositions.Add(currentPosition);

            for (int i = 0; i < corridorCount; i++)
            {
                var corridor = RandomWalkAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
                currentPosition = corridor[^1];

                // Add each end of corridor to the potential room positions
                potentialRoomPositions.Add(currentPosition);

                // Merge the positions with the previous one, but remove duplicate
                floorPositions.UnionWith(corridor);
            }
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
        {
            var roomPositions = new HashSet<Vector2Int>();

            // Get only a percentage count of the potential rooms that can be created
            int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

            // Randomly order the rooms by a generated GUID
            // Return only a specific number of element from the HashSet of potential room positions
            List<Vector2Int> roomToCreates =
                potentialRoomPositions.OrderBy(_ => Guid.NewGuid()).Take(roomToCreateCount).ToList();

            // Create rooms
            foreach (var roomPosition in roomToCreates)
            {
                RunRandomWalk(randomWalkParameters, roomPosition);
                roomPositions.UnionWith(FloorPositions);
            }

            return roomPositions;
        }
    }
}