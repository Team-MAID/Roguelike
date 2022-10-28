using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        /// <summary>
        /// Calculate the centroid for an Enumerable of positions
        /// </summary>
        /// <param name="positions">List of positions to get the centroid from</param>
        /// <returns>Centroid of the list of positions</returns>
        public static Vector2Int GetCentroid(IEnumerable<Vector2Int> positions)
        {
            // Centroid formula for a set of points: https://math.stackexchange.com/questions/1801867/finding-the-centre-of-an-abritary-set-of-points-in-two-dimensions
            var xSum = 0;
            var ySum = 0;

            var positionList = positions.ToList();
            
            // Get the summation of X and Y for every points
            foreach (var position in positionList)
            {
                // Summation of X values
                xSum += position.x;
                // Summation of Y values
                ySum += position.y;
            }

            
            // Calculate average X and Y (which give us the centroid of the points)
            var centroidX = xSum / positionList.Count();
            var centroidY = ySum / positionList.Count();

            return new Vector2Int(centroidX, centroidY);
        }
    }
}