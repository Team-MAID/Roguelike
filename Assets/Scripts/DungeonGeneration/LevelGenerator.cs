using System;
using System.Collections.Generic;
using DungeonGeneration.BSPGeneration;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private BSPDungeonGenerator dungeonGenerator;

        [SerializeField] private GameObject playerPrefab;

        // Enemies
        [Header("Enemy prefabs")]
        [SerializeField] private GameObject batPrefab;

        [SerializeField] private GameObject ratPrefab;
        [SerializeField] private GameObject ghostPrefab;
        [SerializeField] private GameObject spiderNestPrefab;

        // Furniture
        [Header("Furniture prefabs")]
        [SerializeField] private GameObject closetPrefab;

        [SerializeField] private GameObject shopPrefab;

        // Items
        [Header("Item prefabs")]
        [SerializeField] private GameObject healthRefillPotionPrefab;

        [SerializeField] private GameObject attackUpPotionPrefab;
        [SerializeField] private GameObject defenseUpPotionPrefab;
        [SerializeField] private GameObject mysteryPotionPrefab;
        [SerializeField] private GameObject speedPotionPrefab;

        private List<GameObject> _potionPrefabs;

        // Weapons
        [Header("Weapon prefabs")]
        [SerializeField] private GameObject swordPrefab;

        [SerializeField] private GameObject bowPrefab;

        // Instantiated GameObjects in the scene from Prefab
        // =====================================================

        private GameObject _player;

        private List<GameObject> _bat;
        private List<GameObject> _rat;
        private List<GameObject> _ghost;
        private List<GameObject> _spiderNest;

        private List<GameObject> _closet;
        private List<GameObject> _shop;

        private List<GameObject> _potions;
        private List<GameObject> _healthRefillPotion;
        private List<GameObject> _attackUpPotion;
        private List<GameObject> _defenseUpPotion;
        private List<GameObject> _mysteryPotion;
        private List<GameObject> _speedPotion;

        private List<GameObject> _sword;
        private List<GameObject> _bow;

        private void Start()
        {

        }

        private void Init()
        {
            _potionPrefabs = new List<GameObject>
            {
                attackUpPotionPrefab,
                defenseUpPotionPrefab,
                mysteryPotionPrefab,
                speedPotionPrefab
            };
        }

        public void GenerateRandomLevel()
        {
            ClearLevel();
            Init();
            
            dungeonGenerator.GenerateDungeon();

            var generatedLeafs = dungeonGenerator.DungeonTree.Leafs;

            SpawnPlayerInRoom(generatedLeafs[0]);

            foreach (var leaf in generatedLeafs)
            {
                SpawnItemsInRoom(leaf);
            }
        }

        private void SpawnPlayerInRoom(BSPDungeonTreeNode dungeonNode)
        {
            var roomFloors = dungeonNode.Floors;

            Vector2 randomFloorPosition = roomFloors[Random.Range(0, roomFloors.Count - 1)];
            // Add 0.5 for the game object to be instantiated in the center of the tile
            randomFloorPosition += new Vector2(0.5f, 0.5f);

            _player = Instantiate(playerPrefab, randomFloorPosition, Quaternion.identity);
        }

        private void SpawnItemsInRoom(BSPDungeonTreeNode dungeonNode)
        {
            var roomFloors = dungeonNode.Floors;

            int numberOfPotionsToGenerate = Random.Range(0, 4);
            for (var i = 0; i < numberOfPotionsToGenerate; ++i)
            {
                Debug.Log("hello");
                Vector2 randomFloorPosition = roomFloors[Random.Range(0, roomFloors.Count - 1)];
                // Add 0.5 for the game object to be instantiated in the center of the tile
                randomFloorPosition += new Vector2(0.5f, 0.5f);

                GameObject potionToInstantiate = _potionPrefabs[Random.Range(0, _potionPrefabs.Count - 1)];
                GameObject newPotion = Instantiate(potionToInstantiate, randomFloorPosition, Quaternion.identity);
                _potions.Add(newPotion);
            }
        }

        private void ClearLevel()
        {
            DestroyImmediate(_player);
            foreach (var potion in _potions)
            {
                DestroyImmediate(potion);
            }
            _potions.Clear();
        }
    }
}