using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Room
    {
        /// <summary>
        /// Origin x position (left)
        /// </summary>
        private int _x;
    
        /// <summary>
        /// Origin y position (right)
        /// </summary>
        private int _y;

        private readonly int _width;
        private readonly int _height;

        public List<Vector2Int> GridPositions { get; } = new();
        public List<Vector2Int> CornerPositions { get; } = new();
        public List<Vector2Int> OuterWallPositions { get; } = new();
        public Vector2Int ExitPosition { get; private set; }


        public Room(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;

            _width = width;
            _height = height;

            InitialisePositions();
        }

        public Room(Vector2Int position, Vector2Int size)
        {
            _x = position.x;
            _y = position.y;

            _width = size.x;
            _height = size.y;
        
            InitialisePositions();
        }

        /// <summary>
        /// Initialise lists with positions
        /// </summary>
        private void InitialisePositions()
        {
            // Create corner positions for the room
            CornerPositions.Add(new Vector2Int(_x, _y));
            CornerPositions.Add(new Vector2Int(_x, YMax));
            CornerPositions.Add(new Vector2Int(XMax, _y));
            CornerPositions.Add(new Vector2Int(XMax, YMax));

            for (var i = _x; i <= XMax; i++)
            {
                for (var j = _y; j <= YMax; j++)
                {
                    if ((i > _x && i < XMax && (j == _y || j == YMax)) ||
                        (j > _y && j < YMax && (i == _x || i == XMax)))
                    {
                        OuterWallPositions.Add(new Vector2Int(i, j));
                    }

                    if (i != _x && i != XMax && j != _y && j != YMax)
                    {
                        GridPositions.Add(new Vector2Int(i, j));
                    }
                }
            }

            /*ExitPosition = OuterWallPositions[Random.Range(0, OuterWallPositions.Count - 1)];
        OuterWallPositions.Remove(ExitPosition);*/
        }

        public int X => _x;
        public int Y => _y;
    
        public int Height => _height;
        public int Width => _width;

        public int XMax => _x + _width;
        public int YMax => _y + _height;
    }
}