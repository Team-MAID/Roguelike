using UnityEngine;

namespace Editor
{
    using UnityEditor;
    using DungeonGeneration;
    
    [CustomEditor(typeof(LevelGenerator), true)]
    public class LevelGeneratorEditor : Editor
    {
        private LevelGenerator _generator;

        private void Awake()
        {
            _generator = (LevelGenerator) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Random Level"))
            {
                _generator.GenerateRandomLevel();
            }
        }
    }
}