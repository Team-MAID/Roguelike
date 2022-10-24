using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    [Serializable]
    internal struct MinMax
    {
        [Min(0.1f)] public float min;
        public float max;
    }

    public class BSPDungeonGenerator : DungeonGenerator
    {
        [SerializeField] [Tooltip("Minimum/Maximum percentage to split node at a random position")]
        private MinMax splitPositionRange = new() {min = 0.3f, max = 0.6f};

        [SerializeField] [Tooltip("Minimum width of node")]
        private float minNodeWidth = 10f;

        [SerializeField] [Tooltip("Minimum height of node")]
        private float minNodeHeight = 10f;

        [SerializeField] [Tooltip("Minimum room size")]
        private int minRoomWidth = 4;

        [SerializeField] [Tooltip("Minimum room size")]
        private int minRoomHeight = 4;

        [SerializeField] [Tooltip("Root node width")]
        private int dungeonWidth = 100;

        [SerializeField] [Tooltip("Root node height")]
        private int dungeonHeight = 100;

        [SerializeField] [Range(0, 10)] private int offset = 2;

        private BSPDungeonTree _bspDungeonTree;
        private GameObject _roomHolder;

        protected override void RunProceduralGeneration()
        {
            RectInt rootBoundary = new RectInt(startPosition, new Vector2Int(dungeonWidth, dungeonHeight));
            _bspDungeonTree = new BSPDungeonTree(
                rootBoundary,
                minNodeWidth,
                minNodeHeight,
                splitPositionRange.min,
                splitPositionRange.max
            );

            CreateRooms();
        }

        private void CreateRooms()
        {
            HashSet<Vector2Int> floorPositions = CreateSimpleRooms();

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private HashSet<Vector2Int> CreateSimpleRooms()
        {
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

            foreach (var leaf in _bspDungeonTree.Leafs)
            {
                var boundary = leaf.Boundary;
                
                Vector2Int randomSize = new Vector2Int(
                    Random.Range(minRoomWidth, boundary.size.x),
                    Random.Range(minRoomHeight, boundary.size.y)
                );

                Vector2Int origin = new Vector2Int(
                    Random.Range(1, (int) offset),
                    Random.Range(1, (int) offset)
                );

                for (int col = origin.x; col <= randomSize.x; col++)
                {
                    for (int row = origin.y; row <= randomSize.y; row++)
                    {
                        Vector2Int position = boundary.min + new Vector2Int(col, row);
                        floorPositions.Add(position);
                        leaf.floors.Add(position);
                    }
                }
            }

            return floorPositions;
        }

        /*
         * DEBUG METHODS
         */
        private void OnDrawGizmos()
        {
            if (_bspDungeonTree == null) return;

            //DrawDebugTree(_bspTree.Root);
            DrawDebugTreeLeafs();
        }

        private void DrawDebugTreeLeafs()
        {
            foreach (BSPDungeonTreeNode node in _bspDungeonTree.Leafs)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = Color.red;

                //Handles.Label(node.Boundary.center, node.ID.ToString(), style);

                // bottom/left -> top/left
                Gizmos.DrawLine(
                    new Vector2(node.Boundary.xMin, node.Boundary.yMin),
                    new Vector2(node.Boundary.xMin, node.Boundary.yMax)
                );

                // top/left -> top/right
                Gizmos.DrawLine(
                    new Vector2(node.Boundary.xMin, node.Boundary.yMax),
                    new Vector2(node.Boundary.xMax, node.Boundary.yMax)
                );

                // top/right -> bottom/right
                Gizmos.DrawLine(
                    new Vector2(node.Boundary.xMax, node.Boundary.yMax),
                    new Vector2(node.Boundary.xMax, node.Boundary.yMin)
                );

                // bottom/right -> bottom/left
                Gizmos.DrawLine(
                    new Vector2(node.Boundary.xMax, node.Boundary.yMin),
                    new Vector2(node.Boundary.xMin, node.Boundary.yMin)
                );
            }
        }

        private void DrawDebugTree(BSPDungeonTreeNode node)
        {
            Gizmos.color = Color.blue;

            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.red;

            Handles.Label(
                new Vector2(node.Boundary.center.x / style.fontSize, node.Boundary.center.y / style.fontSize),
                node.ID.ToString(), style);

            // bottom/left -> top/left
            Gizmos.DrawLine(
                new Vector2(node.Boundary.xMin, node.Boundary.yMin),
                new Vector2(node.Boundary.xMin, node.Boundary.yMax)
            );

            // top/left -> top/right
            Gizmos.DrawLine(
                new Vector2(node.Boundary.xMin, node.Boundary.yMax),
                new Vector2(node.Boundary.xMax, node.Boundary.yMax)
            );

            // top/right -> bottom/right
            Gizmos.DrawLine(
                new Vector2(node.Boundary.xMax, node.Boundary.yMax),
                new Vector2(node.Boundary.xMax, node.Boundary.yMin)
            );

            // bottom/right -> bottom/left
            Gizmos.DrawLine(
                new Vector2(node.Boundary.xMax, node.Boundary.yMin),
                new Vector2(node.Boundary.xMin, node.Boundary.yMin)
            );

            if (node.LeftChild != null) DrawDebugTree(node.LeftChild);
            if (node.RightChild != null) DrawDebugTree(node.RightChild);
        }
    }
}