using UnityEngine;

namespace Data
{
    // Data used to generate different kind of dungeon for script inhering from the DungeonGenerator abstract class
    [CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkData")]
    public class SimpleRandomWalkScriptableObject : ScriptableObject
    {
        public int iterations = 10, walkLength = 10;
        public bool startRandomlyEachIteration = true;
    }
}