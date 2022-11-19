using UnityEngine;

namespace Editor
{
    using UnityEditor;
    using DungeonGeneration;
    
    [CustomEditor(typeof(BSPDungeonLevelGenerator), true)]
    public class LevelGeneratorEditor : Editor
    {
        private BSPDungeonLevelGenerator _generator;

        private void Awake()
        {
            _generator = (BSPDungeonLevelGenerator) target;
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