using UnityEngine;

namespace Editor
{
    using UnityEditor;
    using DungeonGeneration;
    
    [CustomEditor(typeof(DungeonGenerator), true)]
    public class RandomDungeonGeneratorEditor : Editor
    {
        private DungeonGenerator _generator;

        private void Awake()
        {
            _generator = (DungeonGenerator) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create Dungeon"))
            {
                _generator.GenerateDungeon();
            }
        }
    }
}
