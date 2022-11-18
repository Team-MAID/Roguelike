using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    /// <summary>
    /// Setup and generate a dungeon on a tilemap component
    /// </summary>
    public class BSPDungeonGenerator : DungeonGenerator
    {
        [SerializeField] [Tooltip("Size of the root tree containing each node and room")]
        private Vector2Int dungeonSize = new() {x = 200, y = 200};

        [SerializeField] [Tooltip("Split a node between a two random values")]
        private MinMax splitPosition = new() {min = 0.3f, max = 0.6f};

        [SerializeField] [Tooltip("Minimum size of the node in width and height")]
        private Vector2Int minimumNodeSize = new() {x = 40, y = 40};

        [SerializeField] [Tooltip("Minimum size of a room inside a node (cannot be larger than the node size")]
        private Vector2Int minimumRoomSize = new() {x = 20, y = 20};

        [SerializeField] [Range(0, 5)] [Tooltip("Room offset from the border of the node")]
        private int offset = 2;

        [SerializeField] private bool displayDebugGizmos = true;

        private BSPDungeonTree _bspDungeonTree;

        private GameObject _roomHolder;


        protected override void RunProceduralGeneration()
        {
            RectInt rootBoundary = new RectInt(startPosition, new Vector2Int(dungeonSize.x, dungeonSize.y));
            _bspDungeonTree = new BSPDungeonTree(
                rootBoundary,
                minimumNodeSize.x, minimumNodeSize.y,
                splitPosition.min, splitPosition.max
            );

            CreateRooms();
        }

        private void CreateRooms()
        {
            HashSet<Vector2Int> floorPositions = CreateSimpleRooms();

            HashSet<Vector2Int> corridors = ConnectRooms();
            floorPositions.UnionWith(corridors);

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private HashSet<Vector2Int> ConnectRooms()
        {
            var corridors = new HashSet<Vector2Int>();

            // Get centre position of each room
            var roomCentroids = new List<Vector2Int>();
            foreach (var leaf in _bspDungeonTree.Leafs)
            {
                roomCentroids.Add(leaf.GetRoomCentroid());
            }

            // Get a random room center from the centroid list
            var currentRoomCenter = roomCentroids[Random.Range(0, roomCentroids.Count)];
            roomCentroids.Remove(currentRoomCenter);

            while (roomCentroids.Count > 0)
            {
                // Find closest room center to the current room
                Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCentroids);
                roomCentroids.Remove(closest);

                // Create a corridor between the current and the closeset room
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
                currentRoomCenter = closest;

                // Merge this corridor's floor with all the other corridors
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
                var currentDistance = Vector2Int.Distance(position, currentRoomCenter);
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

                // Set a random size for the room that cannot be larger than the node, but cannot be lower than the minimum room size
                Vector2Int randomSize = new Vector2Int(
                    Random.Range(minimumRoomSize.x, boundary.size.x),
                    Random.Range(minimumRoomSize.y, boundary.size.y)
                );

                // Set a random origin point for the new room from offset to the center of the calculated room size node (in local coordinate node coordinates)
                Vector2Int origin = new Vector2Int(
                    Random.Range(offset, randomSize.x / 2),
                    Random.Range(offset, randomSize.y / 2)
                );

                // Calculate the maximum X,Y positions of the room 
                int xMax = origin.x + randomSize.x;
                int yMax = origin.y + randomSize.y;

                // If the maximum X,Y positions is larger than the size of the node's boundary,
                // we set the maximum to be not larger that the boundary size, minus the offset
                if (xMax >= boundary.size.x)
                {
                    xMax = xMax - origin.x - offset;
                }

                if (yMax >= boundary.size.y)
                {
                    yMax = yMax - origin.y - offset;
                }

                // Generate the floor coordinate for each tiles of the room (scaled in global coordinate of the Scene)
                for (int col = origin.x; col < xMax - offset; col++)
                {
                    for (int row = origin.y; row < yMax - offset; row++)
                    {
                        Vector2Int position = boundary.min + new Vector2Int(col, row);
                        //if (!boundary.Contains(position)) continue;
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
            if (_bspDungeonTree == null || !displayDebugGizmos) return;

            DrawDebugTreeLeafs();
        }

        private void DrawDebugTreeLeafs()
        {
            foreach (BSPDungeonTreeNode node in _bspDungeonTree.Leafs)
            {
#if UNITY_EDITOR
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = Color.red;

                //Handles.Label(node.Boundary.center, node.ID.ToString(), style);
#endif

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

            // This commented code is supposed to show the centre of each room. I keep it for reference for now
            /*Handles.color = Color.blue;
            foreach (var centroid in roomCentroids)
            {
                Handles.DrawSolidDisc((Vector2) centroid, Vector3.forward, 0.5f);
            }*/
        }

        private void DrawDebugTree(BSPDungeonTreeNode node)
        {
            Gizmos.color = Color.blue;

#if UNITY_EDITOR
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.red;

            Handles.Label(
                new Vector2(node.Boundary.center.x / style.fontSize, node.Boundary.center.y / style.fontSize),
                node.ID.ToString(), style);
#endif

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

        // Validate the values in the Inspector (experimental, the conditions might needs to be tweaked, but it works)
        private void OnValidate()
        {
            // Validate minimum room size
            if (minimumRoomSize.x > minimumNodeSize.x)
            {
                minimumRoomSize.x = minimumNodeSize.x;
            }

            if (minimumRoomSize.y > minimumRoomSize.y)
            {
                minimumRoomSize.y = minimumNodeSize.y;
            }

            // Validate split position
            if (splitPosition.min > splitPosition.max)
            {
                splitPosition.min = splitPosition.max;
            }

            if (splitPosition.min < 0) splitPosition.min = 0;
            if (splitPosition.max < 0) splitPosition.max = 0;

            if (splitPosition.min > 1) splitPosition.min = 1;
            if (splitPosition.max > 1) splitPosition.max = 1;
        }
    }
}