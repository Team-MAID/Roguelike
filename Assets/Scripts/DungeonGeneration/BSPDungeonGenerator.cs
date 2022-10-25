using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        private int minNodeWidth = 10;

        [SerializeField] [Tooltip("Minimum height of node")]
        private int minNodeHeight = 10;

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

        private List<Vector2Int> _roomCentroids;

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

            _roomCentroids = new List<Vector2Int>();
            foreach (var leaf in _bspDungeonTree.Leafs)
            {
                var roomFloors = leaf.Floors;
                
                // Centroid formula for a set of points: https://math.stackexchange.com/questions/1801867/finding-the-centre-of-an-abritary-set-of-points-in-two-dimensions
                int xSum = 0;
                int ySum = 0;

                // Get the summation of X and Y for every points
                for (int i = xSum; i < roomFloors.Count; i++)
                {
                    // Summation of X values
                    xSum += roomFloors[i].x;
                    // Summation of Y values
                    ySum += roomFloors[i].y;
                }

                // Calculate average X and Y (which give us the centroid of the points)
                var centroidX = xSum / roomFloors.Count;
                var centroidY = ySum / roomFloors.Count;

                _roomCentroids.Add(new Vector2Int(centroidX, centroidY));
            }

            HashSet<Vector2Int> corridors = ConnectRooms();
            floorPositions.UnionWith(corridors);
            
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private HashSet<Vector2Int> ConnectRooms()
        {
            var corridors = new HashSet<Vector2Int>();
            
            var currentRoomCenter = _roomCentroids[Random.Range(0, _roomCentroids.Count)];
            _roomCentroids.Remove(currentRoomCenter);
            
            while (_roomCentroids.Count > 0)
            {
                Vector2Int closest = FindClosestPointTo(currentRoomCenter, _roomCentroids);
                _roomCentroids.Remove(closest);

                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }

        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            var corridor = new HashSet<Vector2Int>();
            var position = currentRoomCenter;
            corridor.Add(position);
            
            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                }
                else if (destination.y < position.y)
                {
                    position += Vector2Int.down;
                }

                corridor.Add(position);
            }
            
            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }
                else if (destination.x < position.x)
                {
                    position += Vector2Int.left;
                }

                corridor.Add(position);
            }

            return corridor;
        }

        private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            Vector2Int closest = Vector2Int.zero;
            float distance = float.MaxValue;
            foreach (var position in roomCenters)
            {
                float currentDistance = Vector2Int.Distance(position, currentRoomCenter);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = position;
                }
            }

            return closest;
        }

        private HashSet<Vector2Int> CreateSimpleRooms()
        {
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

            foreach (var leaf in _bspDungeonTree.Leafs)
            {
                var boundary = leaf.Boundary;

                Vector2Int origin = new Vector2Int(
                    Random.Range(offset, boundary.size.x / 2),
                    Random.Range(offset, boundary.size.y / 2)
                );

                Vector2Int randomSize = new Vector2Int(
                    Random.Range(origin.x + minRoomWidth, boundary.size.x - offset),
                    Random.Range(origin.y + minRoomHeight, boundary.size.y - offset)
                );

                for (int col = origin.x; col < randomSize.x; col++)
                {
                    for (int row = origin.y; row < randomSize.y; row++)
                    {
                        Vector2Int position = boundary.min + new Vector2Int(col, row);
                        // Position for all the room floor's
                        floorPositions.Add(position);
                        
                        // Add the position to this specific room
                        leaf.Floors.Add(position);
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
            
            /*Handles.color = Color.blue;

            foreach (var centroid in roomCentroids)
            {
                Handles.DrawSolidDisc((Vector2) centroid, Vector3.forward, 0.5f);
            }*/
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