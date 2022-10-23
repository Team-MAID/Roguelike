using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGenerator
{
    public class BSPTreeNode
    {
        private static int _globalID = -1;

        private const int MinSubDungeonSize = 10;

        public int ID { get; }

        /// <summary>
        /// If null, this is the root node
        /// </summary>
        public BSPTreeNode Parent { get; set; }

        /// <summary>
        /// If null, nodes are leafs
        /// </summary>
        public BSPTreeNode LeftChild { get; private set; }

        public BSPTreeNode RightChild { get; private set; }

        public BSPTreeNode Sister { get; private set; }

        public bool Connected;

        /// <summary>
        /// This node boundary
        /// </summary>
        /// <remarks>
        /// X, Y origin of the rectangle = left, bottom
        /// xMin, yMin = left, bottom
        /// xMax, yMax = right, top
        /// </remarks>
        public RectInt SubDungeon { get; }

        /// <summary>
        /// Room in the leaf
        /// </summary>
        private Room _room;

        public List<RectInt> Halls = null;

        public List<Vector2Int> CorridorPositions;
        
        public RectInt Hallway = new();

        public BSPTreeNode(RectInt subDungeon, BSPTreeNode parent = null)
        {
            _globalID++;

            ID = _globalID;

            SubDungeon = subDungeon;

            Parent = parent;

            if (parent == null)
                Sister = null;
        }

        public void Split()
        {
            // Can't split a node that has already been split
            if (LeftChild != null || RightChild != null) return;

            RectInt rectangle1;
            RectInt rectangle2;

            // If width is 25% larger than height, we split vertically
            // If height is 25% larger than width, we split horizontally
            bool horizontal;
            if (SubDungeon.width > SubDungeon.height && (float) SubDungeon.width / SubDungeon.height >= 1.25f)
            {
                horizontal = false;
            }
            else if (SubDungeon.height > SubDungeon.width && (float) SubDungeon.height / SubDungeon.width >= 1.25f)
            {
                horizontal = true;
            }
            else
            {
                horizontal = Random.Range(0f, 1f) > 0.5f;
            }

            // Determine the maximum height or width of a node
            var max = (horizontal ? SubDungeon.height : SubDungeon.width) - MinSubDungeonSize;
            if (max <= MinSubDungeonSize) return;

            // Determine where it is going to split
            float splitPosition = Random.Range(0.3f, 0.6f);

            if (horizontal)
            {
                // Top rectangle
                rectangle1 = new RectInt(SubDungeon.x, SubDungeon.y, SubDungeon.width,
                    Mathf.FloorToInt(SubDungeon.height * splitPosition));
                // Bottom Rectangle
                rectangle2 = new RectInt(SubDungeon.x, SubDungeon.y + rectangle1.height, SubDungeon.width,
                    SubDungeon.height - rectangle1.height);
            }
            else
            {
                // Left rectangle
                rectangle1 = new RectInt(SubDungeon.x, SubDungeon.y, Mathf.FloorToInt(SubDungeon.width * splitPosition),
                    SubDungeon.height);
                // Right rectangle
                rectangle2 = new RectInt(SubDungeon.x + rectangle1.width, SubDungeon.yMin,
                    SubDungeon.width - rectangle1.width, SubDungeon.height);
            }


            LeftChild = new BSPTreeNode(rectangle1, this);
            RightChild = new BSPTreeNode(rectangle2, this);

            LeftChild.Sister = RightChild;
            RightChild.Sister = LeftChild;
        }

        public bool IsLeaf()
        {
            return LeftChild == null && RightChild == null;
        }

        public Room Room
        {
            get
            {
                if (IsLeaf()) return _room;

                /*Room room;
                TraversePreOrder(node =>
                {
                    if (node.IsLeaf())
                });*/

                return _room;
            }
            set => _room = value;
        }
        
        
        /// <summary>
        /// Traverse the tree from Root node
        /// </summary>
        /// <param name="visitingNode">Action to make when visiting a node</param>
        public void TraversePreOrder(Action<BSPTreeNode> visitingNode)
        {
            visitingNode(this);
            // Debug.Log(node.Data.ID);
            LeftChild.TraversePreOrder(visitingNode);
            RightChild.TraversePreOrder(visitingNode);
        }
    }
}