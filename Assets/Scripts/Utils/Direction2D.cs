using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Store a useful list of 2D Directions and method to get random directions
    /// </summary>
    public static class Direction2D
    {
        public static List<Vector2Int> CardinalDirections {get;} = new()
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        public static List<Vector2Int> OrdinalDirections { get; } = new()
        {
            new Vector2Int(-1, 1), // UP-LEFT
            new Vector2Int(1, 1), // UP-RIGHT
            new Vector2Int(1, -1), // DOWN-RIGHT
            new Vector2Int(-1, -1), // DOWN-LEFT
        };
        
        public static List<Vector2Int> AllDirections { get; } = new()
        {
            Vector2Int.up,
            new Vector2Int(1, 1), // UP-RIGHT
            Vector2Int.right,
            new Vector2Int(1, -1), // DOWN-RIGHT
            Vector2Int.down,
            new Vector2Int(-1, -1), // DOWN-LEFT
            Vector2Int.left,
            new Vector2Int(-1, 1), // UP-LEFT
        };
        
        

        public static Vector2Int GetRandomCardinalDirection()
        {
            return CardinalDirections[Random.Range(0, CardinalDirections.Count)];
        }   
        
        public static Vector2Int GetRandomOrdinalDirections()
        {
            return OrdinalDirections[Random.Range(0, OrdinalDirections.Count)];
        }

        public static Vector2Int GetRandomDirection()
        {
            List<Vector2Int> allDirections = CardinalDirections.Concat(OrdinalDirections).ToList();
            return allDirections[Random.Range(0, allDirections.Count)];
        }
    }
}