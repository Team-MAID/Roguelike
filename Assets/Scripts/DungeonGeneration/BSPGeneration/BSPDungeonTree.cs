using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    /// <summary>
    /// BSP Tree storing the dungeon and sub-dungeons (nodes)
    /// </summary>
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

        /*
         * Utils method to traverse the tree
         */
        
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