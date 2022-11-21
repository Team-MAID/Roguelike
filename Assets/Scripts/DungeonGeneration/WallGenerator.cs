using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace DungeonGeneration
{
    public static class WallGenerator
    {
        /// <summary>
        /// Create the walls surrounding the floors passed in parameters
        /// </summary>
        /// <param name="floorPositions">Position of floor tiles</param>
        /// <param name="tilemapVisualizer">Wall will be painted on this tilemap</param>
        public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
        {
            var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.AllDirections);
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