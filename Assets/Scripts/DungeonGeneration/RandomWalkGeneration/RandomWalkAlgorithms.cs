using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace DungeonGeneration
{
    /// <summary>
    /// Implement different Random Walk algorithms that can be used to generate a dungeon
    /// </summary>
    public static class RandomWalkAlgorithms
    {
        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
        {
            var path = new HashSet<Vector2Int>();

            path.Add(startPosition);

            var previousPosition = startPosition;
            
            for (int i = 0; i < walkLength; i++)
            {
                var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
                path.Add(newPosition);
                
                previousPosition = newPosition;
            }

            return path;
        }

        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            var corridor = new List<Vector2Int>();
            var direction = Direction2D.GetRandomCardinalDirection();

            var currentPosition = startPosition;
            corridor.Add(currentPosition);
            for (int i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition); 
            }

            return corridor;
        }
    }
}