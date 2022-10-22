using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace DungeonGenerator
{
    public class Dungeon : MonoBehaviour
    {
        [SerializeField] private GameObject exitTile;
        [SerializeField] private GameObject[] floorTiles;
        [SerializeField] private GameObject[] outerWallTiles;

        private BSPTree _bspTree;

        private GameObject _roomHolder;

        private void Start()
        {
            RectInt rect = new RectInt(0, 0, 250, 250);
            _bspTree = new BSPTree(rect, 4);
            
            GenerateDungeon();

            Debug.Log($"x, y: {rect.x}, {rect.y}");
            Debug.Log($"xMin, yMin: {rect.xMin}, {rect.yMin}");
            Debug.Log($"xMax, yMax: {rect.xMax}, {rect.yMax}");
            Debug.Log($"Center: {rect.center}");
        }
        
        private void GenerateDungeon()
        {

            foreach (BSPTreeNode leaf in _bspTree.Leafs)
            {
                Room room = leaf.Room;
                _roomHolder = new GameObject($"Room {leaf.ID} | ({room.Width}x{room.Height})");

                foreach (Vector2Int position in room.OuterWallPositions)
                {
                    InstantiateOuterWall(position.x, position.y);
                }

                foreach (Vector2Int position in room.GridPositions)
                {
                    InstantiateFloor(position.x, position.y);
                }

                foreach (Vector2Int position in room.CornerPositions)
                {
                    InstantiateOuterWall(position.x, position.y);
                }

                /*var floor = Instantiate(exitTile, new Vector2(room.ExitPosition.x, room.ExitPosition.y),
                    Quaternion.identity);
                floor.transform.SetParent(_roomHolder.transform);*/
            }

        }

        private void InstantiateFloor(int x, int y)
        {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

            var floor = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
            floor.transform.SetParent(_roomHolder.transform);
        }

        private void InstantiateOuterWall(int x, int y)
        {
            var toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

            var outerWall = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
            outerWall.transform.SetParent(_roomHolder.transform);
        }
        
        private void DrawLeafs()
        {
            foreach (BSPTreeNode node in _bspTree.Leafs)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = Color.red;

                Handles.Label(node.SubDungeon.center, node.ID.ToString(), style);

                // bottom/left -> top/left
                Gizmos.DrawLine(
                    new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMin),
                    new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMax)
                );

                // top/left -> top/right
                Gizmos.DrawLine(
                    new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMax),
                    new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMax)
                );

                // top/right -> bottom/right
                Gizmos.DrawLine(
                    new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMax),
                    new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMin)
                );

                // bottom/right -> bottom/left
                Gizmos.DrawLine(
                    new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMin),
                    new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMin)
                );
            }
        }

        private void DrawDebugBSP(BSPTreeNode node)
        {
            Gizmos.color = Color.blue;

            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.red;

            Handles.Label(
                new Vector2(node.SubDungeon.center.x / style.fontSize, node.SubDungeon.center.y / style.fontSize),
                node.ID.ToString(), style);

            // bottom/left -> top/left
            Gizmos.DrawLine(
                new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMin),
                new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMax)
            );

            // top/left -> top/right
            Gizmos.DrawLine(
                new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMax),
                new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMax)
            );

            // top/right -> bottom/right
            Gizmos.DrawLine(
                new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMax),
                new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMin)
            );

            // bottom/right -> bottom/left
            Gizmos.DrawLine(
                new Vector2(node.SubDungeon.xMax, node.SubDungeon.yMin),
                new Vector2(node.SubDungeon.xMin, node.SubDungeon.yMin)
            );

            if (node.LeftChild != null) DrawDebugBSP(node.LeftChild);
            if (node.RightChild != null) DrawDebugBSP(node.RightChild);
        }

        private void OnDrawGizmos()
        {
            if (_bspTree == null) return;

            //DrawDebugBSP(_bspTree.Root);
            DrawLeafs();
        }
    }
}