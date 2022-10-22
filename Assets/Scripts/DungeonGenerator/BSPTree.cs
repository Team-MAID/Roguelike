using System.Collections.Generic;
using UnityEngine;

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
            }
        }

        private void GenerateCorridors()
        {
            foreach (var leaf in Leafs)
            {
                if (leaf.Connected) continue;

                var currentRoomDoorPos = new Vector2Int();
                var sisterRoomDoorPos = new Vector2Int();
                
                var currentRoom = leaf.Room;
                var sisterRoom = leaf.Sister.Room;

                bool connected = false;

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

                    connected = true;
                }

                if (connected)
                {
                    currentRoom.OuterWallPositions.RemoveAll(outerWallPos => currentRoomDoorPos == outerWallPos);
                    sisterRoom.OuterWallPositions.RemoveAll(outerWallPos => sisterRoomDoorPos == outerWallPos);
                    
                    leaf.Connected = true;
                    leaf.Sister.Connected = true;
                    
                    
                }
            }
        }


        /*public int Size { get; private set; } = 0;

        public void TraversePreOrder(BSPTreeNode node)
        {
            if (node == null) return;
            
            // Debug.Log(node.Data.ID);
            TraversePreOrder(node.LeftChild);
            TraversePreOrder(node.RightChild);
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
        }*/
    }
}