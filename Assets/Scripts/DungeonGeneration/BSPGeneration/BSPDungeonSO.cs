using UnityEngine;
using Utils;

namespace DungeonGeneration.BSPGeneration
{
    [CreateAssetMenu(fileName = "NewBSPDungeon", menuName = "Dungeon/BSP Dungeon")]
    public class BSPDungeonSO : ScriptableObject
    {
        [field: Header("Parameters to generate the dungeon. Hover the mouse onto one of it for more details")]
        [field: SerializeField]
        [field: Tooltip("Size of the root tree containing each node and room")]
        public Vector2Int DungeonSize { get; private set; } = new() {x = 60, y = 60};

        [field: Tooltip("Split a node between a two random values")]
        [field: SerializeField]
        public MinMax SplitPosition { get; private set; } = new() {min = 0.3f, max = 0.6f};

        [field: Tooltip("Minimum size of the node in width and height")]
        [field: SerializeField]
        public Vector2Int MinimumNodeSize { get; private set; } = new() {x = 15, y = 15};

        [field: Tooltip("Minimum size of a room inside a node (cannot be larger than the node size")]
        [field: SerializeField]
        public Vector2Int MinimumRoomSize { get; private set; } = new() {x = 10, y = 10};

        [field: Range(1, 5)]
        [field: SerializeField]
        [field: Tooltip("Room offset from the border of the node")]
        public int Offset { get; private set; } = 2;
    }
}