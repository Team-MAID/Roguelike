using System.Collections.Generic;
using Data;
using UnityEngine;

namespace DungeonGeneration
{
    public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
    {
        [SerializeField] private int corridorLength = 14, corridorCount = 5;

        [SerializeField] [Range(0.1f, 1f)] private float roomPercent = .8f;

        [SerializeField] public SimpleRandomWalkScriptableObject roomGenerationParameters;

        protected override void RunProceduralGeneration()
        {
            CorridorFirstGeneration();
        }

        private void CorridorFirstGeneration()
        {
            var floorPositions = new HashSet<Vector2Int>();

            CreateCorridors(floorPositions);
            
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private void CreateCorridors(HashSet<Vector2Int> floorPositions)
        {
            var currentPosition = startPosition;
            for (int i = 0; i < corridorCount; i++)
            {
                var corridor = RandomWalkAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
                currentPosition = corridor[^1];
                
                floorPositions.UnionWith(corridor);
            }
        }
    }
}