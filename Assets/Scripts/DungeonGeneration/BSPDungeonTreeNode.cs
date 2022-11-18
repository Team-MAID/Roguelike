using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    /// <summary>
    /// Node of the BSP Tree, storing data related to a node and to the room stored in this node
    /// </summary>
    public class BSPDungeonTreeNode
    {
        // Global ID of the node, for debugging purposes only, must not be used for anything else than debugging
        private static int _globalID = -1;
        public int ID { get; }

        /// <summary>
        /// If null, parent node is the root node
        /// </summary>
        public BSPDungeonTreeNode Parent { get; }

        /// <summary>
        /// If null, this node is a leaf
        /// </summary>
        public BSPDungeonTreeNode LeftChild { get; private set; }

        /// <summary>
        /// If null, this node is a leaf
        /// </summary>
        public BSPDungeonTreeNode RightChild { get; private set; }

        public BSPDungeonTreeNode Sister { get; private set; }

        /// <summary>
        /// Node boundary
        /// </summary>
        /// <remarks>
        /// X, Y = left, bottom -equivalent to- xMin, yMin = left, bottom
        /// xMax, yMax = right, top
        /// </remarks>
        public RectInt Boundary { get; }

        /// <summary>
        /// Actual room boundary (floor positions)
        /// </summary>
        public List<Vector2Int> Floors { get; } = new();

        public BSPDungeonTreeNode(RectInt boundary, BSPDungeonTreeNode parent = null)
        {
            _globalID++;

            ID = _globalID;

            Parent = parent;

            Boundary = boundary;

            // Ensure that the root node has not sister (because a node with no parent is a root node)
            if (parent == null)
                Sister = null;
        }

        /// <summary>
        /// Split a node horizontally or vertically, at a random position contained between <b><paramref name="minSplitPosition" /></b>
        /// and <b><paramref name="maxSplitPosition" /></b>.
        /// </summary>
        /// <param name="minNodeWidth"></param>
        /// <param name="minNodeHeight"></param>
        /// <param name="minSplitPosition">Minimum position to split in the current node (0.3 is a good value for heterogeneous)</param>
        /// <param name="maxSplitPosition">Maximum position to split in the current (0.6 is a good value for heterogeneous)</param>
        public bool Split(float minNodeWidth, float minNodeHeight, float minSplitPosition, float maxSplitPosition)
        {
            // =====================
            // WARNING: The minimum width and height does not work properly, because, even if the current size of the node
            // is big enough, the position of the split can split in a position that make the new splitted rooms smaller
            // than the minimum.
            // I'd like to fix this problem, however, I do not have the time to do it. It does not cause too much issue, so I let it like that for now.
            // Just something to keep in mind.
            // =====================

            bool horizontal = IsHorizontalSplit();

            // Determine where it is going to split
            float splitPosition = Random.Range(minSplitPosition, maxSplitPosition);


            if (Boundary.size.x > minNodeWidth && Boundary.size.y > minNodeHeight)
            {
                RectInt subBoundary1;
                RectInt subBoundary2;

                if (horizontal)
                {
                    if (Boundary.size.y > minNodeHeight * 2)
                    {
                        var boundaries = SplitHorizontally(splitPosition);
                        subBoundary1 = boundaries[0];
                        subBoundary2 = boundaries[1];
                    }
                    else if (Boundary.size.x > minNodeWidth * 2)
                    {
                        var boundaries = SplitVertically(splitPosition);
                        subBoundary1 = boundaries[0];
                        subBoundary2 = boundaries[1];
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (Boundary.size.x > minNodeWidth * 2)
                    {
                        var boundaries = SplitVertically(splitPosition);
                        subBoundary1 = boundaries[0];
                        subBoundary2 = boundaries[1];
                    }
                    else if (Boundary.size.x > minNodeHeight * 2)
                    {
                        var boundaries = SplitHorizontally(splitPosition);
                        subBoundary1 = boundaries[0];
                        subBoundary2 = boundaries[1];
                    }
                    else
                    {
                        return false;
                    }
                }


                LeftChild = new BSPDungeonTreeNode(subBoundary1, this);
                RightChild = new BSPDungeonTreeNode(subBoundary2, this);

                LeftChild.Sister = RightChild;
                RightChild.Sister = LeftChild;

                return true;
            }


            return false;
        }

        public Vector2Int GetRoomCentroid()
        {
            return VectorUtils.GetCentroid(Floors);
        }

        public bool IsLeaf()
        {
            return LeftChild == null || RightChild == null;
        }


        /// <summary>
        /// Traverse the tree from Root node
        /// </summary>
        /// <param name="visitingNode">Action to make when visiting a node</param>
        public void TraversePreOrder(Action<BSPDungeonTreeNode> visitingNode)
        {
            visitingNode(this);
            LeftChild.TraversePreOrder(visitingNode);
            RightChild.TraversePreOrder(visitingNode);
        }

        private bool IsHorizontalSplit()
        {
            // If width is 25% larger than height, we split vertically
            // If height is 25% larger than width, we split horizontally
            bool horizontal;
            if (Boundary.width > Boundary.height && (float) Boundary.width / Boundary.height >= 1.25f)
            {
                horizontal = false;
            }
            else if (Boundary.height > Boundary.width && (float) Boundary.height / Boundary.width >= 1.25f)
            {
                horizontal = true;
            }
            else
            {
                horizontal = Random.Range(0f, 1f) > 0.5f;
            }

            return horizontal;
        }

        private RectInt[] SplitHorizontally(float splitPosition)
        {
            // Top rectangle
            var subBoundary1 = new RectInt(Boundary.x, Boundary.y, Boundary.width,
                Mathf.FloorToInt(Boundary.height * splitPosition));
            // Bottom Rectangle
            var subBoundary2 = new RectInt(Boundary.x, Boundary.y + subBoundary1.height, Boundary.width,
                Boundary.height - subBoundary1.height);

            return new[] {subBoundary1, subBoundary2};
        }

        private RectInt[] SplitVertically(float splitPosition)
        {
            // Left rectangle
            var subBoundary1 = new RectInt(Boundary.x, Boundary.y, Mathf.FloorToInt(Boundary.width * splitPosition),
                Boundary.height);
            // Right rectangle
            var subBoundary2 = new RectInt(Boundary.x + subBoundary1.width, Boundary.yMin,
                Boundary.width - subBoundary1.width, Boundary.height);

            return new[] {subBoundary1, subBoundary2};
        }
    }
}