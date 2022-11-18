using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace DungeonGeneration.RandomWalkGeneration
{
    public class SimpleRandomWalkDungeonGenerator : DungeonGenerator
    {
        [SerializeField]
        protected SimpleRandomWalkScriptableObject randomWalkParameters;

        protected override void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkScriptableObject parameters, Vector2Int position)
        {
            var currentPosition = position;

            var floorPositions = new HashSet<Vector2Int>();

            for (int i = 0; i < parameters.iterations; i++)
            {
                var path = RandomWalkAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
                
                floorPositions.UnionWith(path);

                if (parameters.startRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }

            return floorPositions;
        }
    }
}