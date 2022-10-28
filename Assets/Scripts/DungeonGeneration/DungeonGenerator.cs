using UnityEngine;

namespace DungeonGeneration
{
    /// <summary>
    /// Common properties and methods to generate a procedural dungeon
    /// </summary>
    public abstract class DungeonGenerator : MonoBehaviour
    {
        [SerializeField] protected TilemapVisualizer tilemapVisualizer;
        [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

        public void GenerateDungeon()
        {
            tilemapVisualizer.Clear();
            RunProceduralGeneration();
        }

        protected abstract void RunProceduralGeneration();
    }
}
