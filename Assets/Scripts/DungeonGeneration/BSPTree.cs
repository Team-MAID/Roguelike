using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGenerator
{
    public class BSPTree
    {
        public BSPTreeNode Root { get; } = null;

        public List<BSPTreeNode> Leafs { get; } = new();

        public BSPTree(RectInt rectangle, int numberOfSplit)
        {
            Root = new BSPTreeNode(rectangle);

            BuildTree(Root, numberOfSplit);
            GenerateRoom();
            GenerateCorridors();
        }

        private void BuildTree(BSPTreeNode node, int numberOfSplit)
        {
            if (numberOfSplit == 0)
            {
                Leafs.Add(node);
                return;
            }

            node.Split();

            BuildTree(node.LeftChild, numberOfSplit - 1);
            BuildTree(node.RightChild, numberOfSplit - 1);
        }

        private void GenerateRoom()
        {
            // Generate room positions for each leaf
            foreach (BSPTreeNode leaf in Leafs)
            {
                RectInt subDungeon = leaf.SubDungeon;

                Vector2Int randomOrigin = new Vector2Int(
                    Random.Range(subDungeon.xMin + 1, Mathf.FloorToInt(subDungeon.center.x)),
                    Random.Range(subDungeon.yMin + 1, Mathf.FloorToInt(subDungeon.center.y))
                );

                Vector2Int size = new Vector2Int(
                    Mathf.FloorToInt((subDungeon.xMax - randomOrigin.x) * 0.8f),
                    Mathf.FloorToInt((subDungeon.yMax - randomOrigin.y) * 0.8f)
                );

                leaf.Room = new Room(randomOrigin, size);

                /*Vector2Int randomSize = new Vector2Int(
                    Random.Range(10, subDungeon.width - 2),
                    Random.Range(10, subDungeon.height - 2)
                );

                Vector2Int randomOrigin = new Vector2Int(
                    Random.Range(1, subDungeon.width - randomSize.x - 1),
                    Random.Range(1, subDungeon.height - randomSize.y - 1)
                );


                leaf.Room = new Room(subDungeon.x + randomOrigin.x, subDungeon.y + randomOrigin.y, randomSize.x,
                    randomSize.y);*/
            }
        }

        private void GenerateCorridors()
        {
            /*foreach (var leaf in Leafs)
            {
                var currentRoom = leaf.Room;
                var sisterRoom = leaf.Sister.Room;

                if (Random.Range(0f, 1f) > 0.5f)
                {
                    for (int x = Mathf.Min(currentRoom.X, sisterRoom.X);
                         x < Mathf.Max(currentRoom.X, sisterRoom.X);
                         x++)
                    {
                        
                    }
                }
            }*/

            foreach (var leaf in Leafs)
            {
                if (leaf.Connected) continue;

                var currentRoomDoorPos = new Vector2Int();
                var sisterRoomDoorPos = new Vector2Int();

                var currentRoom = leaf.Room;
                var sisterRoom = leaf.Sister.Room;

                bool connected = false;

                var hallway = new RectInt();

                // X sides are face-to-face (Overlap in X)
                if (sisterRoom.X < currentRoom.XMax && sisterRoom.XMax > currentRoom.X)
                {
                    int minX = 0;
                    int maxX = 0;

                    if (sisterRoom.X > currentRoom.X)
                    {
                        minX = sisterRoom.X;
                    }
                    else
                    {
                        minX = currentRoom.X;
                    }

                    if (sisterRoom.XMax > currentRoom.XMax)
                    {
                        maxX = currentRoom.XMax;
                    }
                    else
                    {
                        maxX = sisterRoom.XMax;
                    }

                    int randomDoorPosX = Random.Range(minX, maxX);
                    int currentRoomPosY;
                    int sisterRoomPosY;

                    // Sister is to Top direction
                    if (sisterRoom.Y > currentRoom.Y)
                    {
                        currentRoomPosY = currentRoom.YMax;
                        sisterRoomPosY = sisterRoom.Y;
                    }
                    // Sister is to Bottom direction
                    else
                    {
                        currentRoomPosY = currentRoom.Y;
                        sisterRoomPosY = sisterRoom.YMax;
                    }

                    currentRoomDoorPos = new Vector2Int(randomDoorPosX, currentRoomPosY);
                    sisterRoomDoorPos = new Vector2Int(randomDoorPosX, sisterRoomPosY);

                    if (sisterRoom.Y > currentRoom.Y)
                    {
                        hallway = new RectInt(currentRoomDoorPos, sisterRoomDoorPos - currentRoomDoorPos);
                    }
                    else
                    {
                        hallway = new RectInt(currentRoomDoorPos, currentRoomDoorPos - sisterRoomDoorPos);
                    }

                    currentRoom.OuterWallPositions.RemoveAll(outerWallPos => currentRoomDoorPos == outerWallPos);
                    sisterRoom.OuterWallPositions.RemoveAll(outerWallPos => sisterRoomDoorPos == outerWallPos);

                    connected = true;
                }

                // Y sides are face-to-face (Overlap in Y)
                if (sisterRoom.Y < currentRoom.YMax && sisterRoom.YMax > currentRoom.Y)
                {
                    int minY = 0;
                    int maxY = 0;

                    if (sisterRoom.Y > currentRoom.Y)
                    {
                        minY = sisterRoom.Y;
                    }
                    else
                    {
                        minY = currentRoom.Y;
                    }

                    if (sisterRoom.YMax > currentRoom.YMax)
                    {
                        maxY = currentRoom.YMax;
                    }
                    else
                    {
                        maxY = sisterRoom.YMax;
                    }

                    int randomDoorPosY = Random.Range(minY, maxY);
                    int currentRoomPosX;
                    int sisterRoomPosX;

                    // Sister is to Top direction
                    if (sisterRoom.X > currentRoom.X)
                    {
                        currentRoomPosX = currentRoom.XMax;
                        sisterRoomPosX = sisterRoom.X;
                    }
                    // Sister is to Bottom direction
                    else
                    {
                        currentRoomPosX = currentRoom.X;
                        sisterRoomPosX = sisterRoom.XMax;
                    }

                    currentRoomDoorPos = new Vector2Int(currentRoomPosX, randomDoorPosY);
                    sisterRoomDoorPos = new Vector2Int(sisterRoomPosX, randomDoorPosY);

                    if (sisterRoom.X > currentRoom.X)
                    {
                        //corridor = new Room();
                        hallway = new RectInt(currentRoomDoorPos, sisterRoomDoorPos - currentRoomDoorPos);
                    }
                    else
                    {
                        hallway = new RectInt(currentRoomDoorPos, currentRoomDoorPos - sisterRoomDoorPos);
                    }

                    connected = true;
                }

                if (connected)
                {
                    currentRoom.OuterWallPositions.RemoveAll(outerWallPos => currentRoomDoorPos == outerWallPos);
                    sisterRoom.OuterWallPositions.RemoveAll(outerWallPos => sisterRoomDoorPos == outerWallPos);

                    leaf.Parent.Hallway = hallway;

                    leaf.Connected = true;
                    leaf.Sister.Connected = true;
                }
            }

            /*foreach (var leaf in Leafs)
            {
                var halls = new List<RectInt>();

                var currentRoom = leaf.Room;
                var sisterRoom = leaf.Sister.Room;

                var point1 = new Vector2Int(Random.Range(currentRoom.X + 1, currentRoom.XMax - 2),
                    Random.Range(currentRoom.YMax + 1, currentRoom.Y - 2));
                var point2 = new Vector2Int(Random.Range(sisterRoom.X + 1, sisterRoom.XMax - 2),
                    Random.Range(sisterRoom.YMax + 1, sisterRoom.Y - 2));

                var width = point2.x - point1.x;
                var height = point2.y - point1.y;

                if (width < 0)
                {
                    if (height < 0)
                    {
                        if (Random.Range(0f, 1f) < 0.5f)
                        {
                            halls.Add(new RectInt(point2.x, point1.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point2.x, point2.y, 1, Mathf.Abs(height)));
                        }
                        else
                        {
                            halls.Add(new RectInt(point2.x, point2.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point1.x, point2.y, 1, Mathf.Abs(height)));
                        }
                    }
                    else if (height > 0)
                    {
                        if (Random.Range(0f, 1f) < 0.5f)
                        {
                            halls.Add(new RectInt(point2.x, point1.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point2.x, point1.y, 1, Mathf.Abs(height)));
                        }
                        else
                        {
                            halls.Add(new RectInt(point2.x, point2.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point1.x, point1.y, 1, Mathf.Abs(height)));
                        }
                    }
                    else
                    {
                        halls.Add(new RectInt(point2.x, point2.y, Mathf.Abs(width), 1));
                    }
                }
                else if (width > 0)
                {
                    if (height < 0)
                    {
                        if (Random.Range(0f, 1f) < 0.5f)
                        {
                            halls.Add(new RectInt(point1.x, point2.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point1.x, point2.y, 1, Mathf.Abs(height)));
                        }
                        else
                        {
                            halls.Add(new RectInt(point1.x, point1.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point2.x, point2.y, 1, Mathf.Abs(height)));
                        }
                    }
                    else if (height > 0)
                    {
                        if (Random.Range(0f, 1f) < 0.5f)
                        {
                            halls.Add(new RectInt(point1.x, point1.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point2.x, point1.y, 1, Mathf.Abs(height)));
                        }
                        else
                        {
                            halls.Add(new RectInt(point1.x, point2.y, Mathf.Abs(width), 1));
                            halls.Add(new RectInt(point1.x, point1.y, 1, Mathf.Abs(height)));
                        }
                    }
                    else // if (h == 0)
                    {
                        halls.Add(new RectInt(point1.x, point1.y, Mathf.Abs(width), 1));
                    }
                }
                else // if (w == 0)
                {
                    if (height < 0)
                    {
                        halls.Add(new RectInt(point2.x, point2.y, 1, Mathf.Abs(height)));
                    }
                    else if (height > 0)
                    {
                        halls.Add(new RectInt(point1.x, point1.y, 1, Mathf.Abs(height)));
                    }
                }

                leaf.Parent.Halls = halls;
            }*/
        }

        /// <summary>
        /// Traverse the tree from Root node
        /// </summary>
        /// <param name="visitingNode">Action to make when visiting a node</param>
        public void TraversePreOrder(Action<BSPTreeNode> visitingNode)
        {
            if (Root == null) return;

            visitingNode(Root);
            // Debug.Log(node.Data.ID);
            TraversePreOrder(Root.LeftChild, visitingNode);
            TraversePreOrder(Root.RightChild, visitingNode);
        }

        /// <summary>
        /// Traverse the tree from passed Node
        /// </summary>
        /// <param name="node">Traversal start from this node</param>
        /// <param name="visitingNode">Action to make when visiting a node</param>
        private void TraversePreOrder(BSPTreeNode node, Action<BSPTreeNode> visitingNode)
        {
            if (node == null) return;

            visitingNode(node);
            // Debug.Log(node.Data.ID);
            TraversePreOrder(node.LeftChild, visitingNode);
            TraversePreOrder(node.RightChild, visitingNode);
        }

        public void TraverseInOrder(BSPTreeNode node)
        {
            if (node == null) return;

            TraverseInOrder(node.LeftChild);
            // Debug.Log(node.Data.ID);
            TraverseInOrder(node.RightChild);
        }

        public void TraversePostOrder(BSPTreeNode node)
        {
            if (node == null) return;

            TraversePostOrder(node.LeftChild);
            TraversePostOrder(node.RightChild);
            // Debug.Log(node.Data.ID);
        }

        private List<RectInt> CreateHall(Room currentRoom, Room sisterRoom)
        {
            var halls = new List<RectInt>();

            /*var currentRoom = leaf.Room;
                var sisterRoom = leaf.Sister.Room;*/

            var point1 = new Vector2Int(Random.Range(currentRoom.X + 1, currentRoom.XMax - 2),
                Random.Range(currentRoom.YMax + 1, currentRoom.Y - 2));
            var point2 = new Vector2Int(Random.Range(sisterRoom.X + 1, sisterRoom.XMax - 2),
                Random.Range(sisterRoom.YMax + 1, sisterRoom.Y - 2));

            var width = point2.x - point1.x;
            var height = point2.y - point1.y;

            if (width < 0)
            {
                if (height < 0)
                {
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        halls.Add(new RectInt(point2.x, point1.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point2.x, point2.y, 1, Mathf.Abs(height)));
                    }
                    else
                    {
                        halls.Add(new RectInt(point2.x, point2.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point1.x, point2.y, 1, Mathf.Abs(height)));
                    }
                }
                else if (height > 0)
                {
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        halls.Add(new RectInt(point2.x, point1.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point2.x, point1.y, 1, Mathf.Abs(height)));
                    }
                    else
                    {
                        halls.Add(new RectInt(point2.x, point2.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point1.x, point1.y, 1, Mathf.Abs(height)));
                    }
                }
                else
                {
                    halls.Add(new RectInt(point2.x, point2.y, Mathf.Abs(width), 1));
                }
            }
            else if (width > 0)
            {
                if (height < 0)
                {
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        halls.Add(new RectInt(point1.x, point2.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point1.x, point2.y, 1, Mathf.Abs(height)));
                    }
                    else
                    {
                        halls.Add(new RectInt(point1.x, point1.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point2.x, point2.y, 1, Mathf.Abs(height)));
                    }
                }
                else if (height > 0)
                {
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        halls.Add(new RectInt(point1.x, point1.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point2.x, point1.y, 1, Mathf.Abs(height)));
                    }
                    else
                    {
                        halls.Add(new RectInt(point1.x, point2.y, Mathf.Abs(width), 1));
                        halls.Add(new RectInt(point1.x, point1.y, 1, Mathf.Abs(height)));
                    }
                }
                else // if (h == 0)
                {
                    halls.Add(new RectInt(point1.x, point1.y, Mathf.Abs(width), 1));
                }
            }
            else // if (w == 0)
            {
                if (height < 0)
                {
                    halls.Add(new RectInt(point2.x, point2.y, 1, Mathf.Abs(height)));
                }
                else if (height > 0)
                {
                    halls.Add(new RectInt(point1.x, point1.y, 1, Mathf.Abs(height)));
                }
            }


            return halls;
        }
    }
}