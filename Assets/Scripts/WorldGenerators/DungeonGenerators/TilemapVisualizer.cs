using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DungeonGeneration
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap floorTilemap, wallTilemap;
    
        [SerializeField]
        private List<TileBase> floorTiles;

        [SerializeField] private TileBase wallTop;

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
        {
            PaintTiles(floorPositions, floorTilemap, floorTiles);
        }

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, List<TileBase> tiles)
        {
            foreach (var position in positions)
            {
                var randomTile = tiles[Random.Range(0, tiles.Count)];
            
                PaintSingleTile(tilemap, randomTile, position);
            }
        }

        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition, tile);
        }

        public void PaintSingleBasicWall(Vector2Int position)
        {
            PaintSingleTile(wallTilemap, wallTop, position);
        }
        
        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
        }
    }
}
