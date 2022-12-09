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
        
        public HashSet<Vector2Int> FloorPositions { get; private set; }

        protected override void RunProceduralGeneration()
        {
            RunRandomWalk(randomWalkParameters, startPosition);
            
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(FloorPositions);
            WallGenerator.CreateWalls(FloorPositions, tilemapVisualizer);
        }

        protected void RunRandomWalk(SimpleRandomWalkScriptableObject parameters, Vector2Int position)
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

            FloorPositions = floorPositions;
        }
    }
}