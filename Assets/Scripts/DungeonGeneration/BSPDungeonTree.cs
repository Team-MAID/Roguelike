using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    public class BSPDungeonTree
    {
        public BSPDungeonTreeNode Root { get; }

        public List<BSPDungeonTreeNode> Leafs { get; } = new();

        private readonly float _minNodeWidth;
        private readonly float _minNodeHeight;

        private readonly float _minSplitPosition;
        private readonly float _maxSplitPosition;

        public BSPDungeonTree(RectInt rootBoundary, float minNodeWidth, float minNodeHeight, float minSplitPosition,
            float maxSplitPosition)
        {
            _minNodeWidth = minNodeWidth;
            _minNodeHeight = minNodeHeight;

            _minSplitPosition = minSplitPosition;
            _maxSplitPosition = maxSplitPosition;

            Root = new BSPDungeonTreeNode(rootBoundary);

            // Build BSP from Root
            BuildTree(Root);
        }

        private void BuildTree(BSPDungeonTreeNode node)
        {
            bool split = node.Split(_minNodeWidth, _minNodeHeight, _minSplitPosition, _maxSplitPosition);
            
            if (!split)
            {
                Leafs.Add(node);
                return;
            }

            // Call the function again for the new children of the split node
            BuildTree(node.LeftChild);
            BuildTree(node.RightChild);
        }

        private void GenerateRoom()
        {
            // Generate room positions for each leaf
            foreach (BSPDungeonTreeNode leaf in Leafs)
            {
                RectInt subDungeon = leaf.Boundary;

                Vector2Int randomOrigin = new Vector2Int(
                    Random.Range(subDungeon.xMin + 1, Mathf.FloorToInt(subDungeon.center.x)),
                    Random.Range(subDungeon.yMin + 1, Mathf.FloorToInt(subDungeon.center.y))
                );

                Vector2Int size = new Vector2Int(
                    Mathf.FloorToInt((subDungeon.xMax - randomOrigin.x) * 0.8f),
                    Mathf.FloorToInt((subDungeon.yMax - randomOrigin.y) * 0.8f)
                );

                //leaf.Room = new RectInt(randomOrigin, size);

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

        /// <summary>
        /// Traverse the tree from Root node
        /// </summary>
        /// <param name="visitingNode">Action to make when visiting a node</param>
        public void TraversePreOrder(Action<BSPDungeonTreeNode> visitingNode)
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
        private void TraversePreOrder(BSPDungeonTreeNode node, Action<BSPDungeonTreeNode> visitingNode)
        {
            if (node == null) return;

            visitingNode(node);
            // Debug.Log(node.Data.ID);
            TraversePreOrder(node.LeftChild, visitingNode);
            TraversePreOrder(node.RightChild, visitingNode);
        }

        public void TraverseInOrder(BSPDungeonTreeNode node)
        {
            if (node == null) return;

            TraverseInOrder(node.LeftChild);
            // Debug.Log(node.Data.ID);
            TraverseInOrder(node.RightChild);
        }

        public void TraversePostOrder(BSPDungeonTreeNode node)
        {
            if (node == null) return;

            TraversePostOrder(node.LeftChild);
            TraversePostOrder(node.RightChild);
            // Debug.Log(node.Data.ID);
        }
    }
}