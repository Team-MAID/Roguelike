using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Direction2D
    {
        public static List<Vector2Int> CardinalDirections {get;} = new List<Vector2Int>()
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return CardinalDirections[Random.Range(0, CardinalDirections.Count)];
        }
    }
}