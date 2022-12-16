using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public abstract class LevelGenerator : MonoBehaviour
    {
        [SerializeField] protected DungeonGenerator DungeonGenerator;
        
        [SerializeField] protected GameObject playerPrefab;
        [SerializeField] protected GameObject companionPrefab;

        [Header("Enemy Spawner")]
        [SerializeField] protected GameObject spawnerPrefab;
        
        // Furniture
        [Header("Furniture prefabs")]
        [SerializeField] protected GameObject closetPrefab;
        [SerializeField] protected GameObject shopPrefab;

        // Items
        //[Header("Item prefabs")]
        //[SerializeField] protected GameObject healthRefillPotionPrefab;
        //[SerializeField] protected GameObject attackUpPotionPrefab;
        //[SerializeField] protected GameObject defenseUpPotionPrefab;
        //[SerializeField] protected GameObject mysteryPotionPrefab;
        //[SerializeField] protected GameObject speedPotionPrefab;

        // Weapons
        [Header("Weapon prefabs")]
        [SerializeField] protected GameObject swordPrefab;
        [SerializeField] protected GameObject bowPrefab;

        // Staircase
        [Header("Staircase prefab")]
        [SerializeField] protected GameObject staircasePrefab;
        
        protected List<GameObject> _potionPrefabs;
        
        // Instantiated GameObjects in the scene from Prefab
        // =====================================================

        protected readonly List<GameObject> _generatedGO = new();

        private List<GameObject> _sword;
        private List<GameObject> _bow;
        
        private void Init()
        {
            //_potionPrefabs = new List<GameObject>
            //{
            //    healthRefillPotionPrefab,
            //    attackUpPotionPrefab,
            //    defenseUpPotionPrefab,
            //    mysteryPotionPrefab,
            //    speedPotionPrefab
            //};
        }

        protected virtual void GenerateRandomLevel()
        {
            ClearLevel();
            Init();
        }
        
        private void ClearLevel()
        {
            foreach (var go in _generatedGO)
            {
                Destroy(go);
            }
            _generatedGO.Clear();
        }
    }
}