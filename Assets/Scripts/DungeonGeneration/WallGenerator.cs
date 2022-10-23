using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace DungeonGeneration
{
    public static class WallGenerator
    {
        public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
        {
            var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.CardinalDirections);
            foreach (var position in basicWallPositions)
            {
                tilemapVisualizer.PaintSingleBasicWall(position);
            }
        }

        private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
        {
            var wallPositions = new HashSet<Vector2Int>();
            foreach (var position in floorPositions)
            {
                foreach (var direction in directions)
                {
                    var neighbourPosition = position + direction;
                    if (!floorPositions.Contains(neighbourPosition))
                    {
                        wallPositions.Add(neighbourPosition);
                    }
                }
            }

            return wallPositions;
        }
    }
}