using UnityEngine;

namespace Editor
{
    using UnityEditor;
    using DungeonGeneration;
    
    [CustomEditor(typeof(BSPLevelGenerator), true)]
    public class LevelGeneratorEditor : Editor
    {
        private BSPLevelGenerator _generator;

        private void Awake()
        {
            _generator = (BSPLevelGenerator) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            /*if (GUILayout.Button("Generate Random Level"))
            {
                _generator.GenerateRandomLevel();
            }*/
        }
    }
}