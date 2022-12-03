using System;
using System.Collections.Generic;
using System.Linq;
using DungeonGeneration.BSPGeneration;
using ExtensionMethods;
using InventorySystem.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    /// <summary>
    /// Generate the full level contained inside the dungeon (items, enemies, etc.)
    /// </summary>
    public class BSPDungeonLevelGenerator : MonoBehaviour
    {
        [SerializeField] private BSPDungeonGenerator dungeonGenerator;

        [SerializeField] private GameObject playerPrefab;

        // Enemies
        [Header("Enemy prefabs")]
        [SerializeField] private GameObject spawnerPrefab;
        //[SerializeField] private GameObject batPrefab;
        //[SerializeField] private GameObject ratPrefab;
        //[SerializeField] private GameObject ghostPrefab;
        //[SerializeField] private GameObject spiderNestPrefab;

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

        // Weapons
        [Header("Weapon prefabs")]
        [SerializeField] private GameObject swordPrefab;

        [SerializeField] private GameObject bowPrefab;

        // Staircase
        [Header("Staircase prefab")]
        [SerializeField] private GameObject staircasePrefab;

        private List<GameObject> _potionPrefabs;
        private List<GameObject> _enemyPrefabs;

        // Instantiated GameObjects in the scene from Prefab
        // =====================================================

        private readonly List<GameObject> _generatedGO = new();

        private List<GameObject> _sword;
        private List<GameObject> _bow;

        private void Start()
        {
            GenerateRandomLevel();
        }

        private void Init()
        {
            _potionPrefabs = new List<GameObject>
            {
                healthRefillPotionPrefab,
                attackUpPotionPrefab,
                defenseUpPotionPrefab,
                mysteryPotionPrefab,
                speedPotionPrefab
            };

            _enemyPrefabs = new List<GameObject>
            {
                spawnerPrefab
                //batPrefab,
                //ratPrefab,
                //ghostPrefab,
                //spiderNestPrefab
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
                SpawnClosetInRoom(leaf);
                SpawnEnemiesInRoom(leaf);
                GenerateShopInRoom(leaf);
            }
            
            GenerateStaircase(generatedLeafs.Last());
        }

        private void SpawnPlayerInRoom(BSPDungeonTreeNode dungeonNode)
        {
            var roomFloors = dungeonNode.Floors;

            Vector2 randomFloorPosition = roomFloors[Random.Range(0, roomFloors.Count - 1)];
            // Add 0.5 for the game object to be instantiated in the center of the tile
            randomFloorPosition += new Vector2(0.5f, 0.5f);

            //_player = Instantiate(playerPrefab, randomFloorPosition, Quaternion.identity);
            playerPrefab.transform.position = randomFloorPosition;
            playerPrefab.SetActive(true);
            
            /*var mainCamera = Camera.main;
            if (mainCamera)
            {
                var cameraController = mainCamera.GetComponent<CameraController>();
                cameraController.Player = playerPrefab;
            }*/
        }

        private void SpawnItemsInRoom(BSPDungeonTreeNode dungeonNode)
        {
            var roomFloors = dungeonNode.Floors;

            int numberOfPotionsToGenerate = Random.Range(1, 4);
            for (var i = 0; i < numberOfPotionsToGenerate; ++i)
            {
                Vector2 randomFloorPosition = roomFloors[Random.Range(0, roomFloors.Count - 1)];
                // Add 0.5 for the game object to be instantiated in the center of the tile
                randomFloorPosition += new Vector2(0.5f, 0.5f);

                GameObject potionToInstantiate = _potionPrefabs[Random.Range(0, _potionPrefabs.Count - 1)];
                GameObject newPotion = Instantiate(potionToInstantiate, randomFloorPosition, Quaternion.identity);
                _generatedGO.Add(newPotion);
            }
        }

        private void SpawnClosetInRoom(BSPDungeonTreeNode dungeonNode)
        {
            var isGenerateCloset = Random.value > 0.5f;
            if (isGenerateCloset)
            {
                var roomFloors = dungeonNode.Floors;

                int minX = roomFloors.Min(pos => pos.x);
                int maxX = roomFloors.Max(pos => pos.x);
                int minY = roomFloors.Min(pos => pos.y);
                int maxY = roomFloors.Max(pos => pos.y);

                Vector2 closetPosition;

                var isTopSide = Random.value > 0.5f;
                var isRightSide = Random.value > 0.5f;
                var isBottomSide = Random.value > 0.5f;
                var isLeftSide = Random.value > 0.5f;

                if (isTopSide)
                {
                    closetPosition = new(Random.Range(minX, maxX) + 0.5f, maxY);
                    _generatedGO.Add(Instantiate(closetPrefab, closetPosition, Quaternion.identity));
                    return;
                }

                if (isRightSide)
                {
                    closetPosition = new(maxX + 0.5f, Random.Range(minY, maxY));
                    _generatedGO.Add(Instantiate(closetPrefab, closetPosition, Quaternion.identity));
                    return;
                }

                if (isBottomSide)
                {
                    closetPosition = new(Random.Range(minX, maxX) + 0.5f, minY);
                    _generatedGO.Add(Instantiate(closetPrefab, closetPosition, Quaternion.identity));
                    return;
                }

                if (isLeftSide)
                {
                    closetPosition = new(minX + 0.5f, Random.Range(minY, maxY));
                    _generatedGO.Add(Instantiate(closetPrefab, closetPosition, Quaternion.identity));
                    return;
                }
            }
        }

        private void SpawnEnemiesInRoom(BSPDungeonTreeNode dungeonNode)
        {
            var roomFloors = dungeonNode.Floors;

            int numberOfEnemiesToGenerate = 1;// Random.Range(1, 3);
            for (var i = 0; i < numberOfEnemiesToGenerate; ++i)
            {
                Vector2 randomFloorPosition = roomFloors[Random.Range(0, roomFloors.Count - 1)];
                // Add 0.5 for the game object to be instantiated in the center of the tile
                randomFloorPosition += new Vector2(0.5f, 0.5f);

                GameObject enemyToInstantiate = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count - 1)];
                GameObject newEnemy = Instantiate(enemyToInstantiate, randomFloorPosition, Quaternion.identity);
                _generatedGO.Add(newEnemy);
            }
        }

        private void GenerateShopInRoom(BSPDungeonTreeNode dungeonNode)
        {
            var isGenerateShop = Random.value > 0.5f;
            if (isGenerateShop)
            {
                var roomFloors = dungeonNode.Floors;

                int minX = roomFloors.Min(pos => pos.x);
                int maxX = roomFloors.Max(pos => pos.x);
                int minY = roomFloors.Min(pos => pos.y);
                int maxY = roomFloors.Max(pos => pos.y);

                Vector2 shopPosition;

                var isTopSide = Random.value > 0.5f;
                var isRightSide = Random.value > 0.5f;
                var isBottomSide = Random.value > 0.5f;
                var isLeftSide = Random.value > 0.5f;

                if (isTopSide)
                {
                    shopPosition = new(Random.Range(minX, maxX) + 0.5f, maxY);
                    _generatedGO.Add(Instantiate(shopPrefab, shopPosition, Quaternion.identity));
                    return;
                }

                if (isRightSide)
                {
                    shopPosition = new(maxX + 0.5f, Random.Range(minY, maxY));
                    _generatedGO.Add(Instantiate(shopPrefab, shopPosition, Quaternion.identity));
                    return;
                }

                if (isBottomSide)
                {
                    shopPosition = new(Random.Range(minX, maxX) + 0.5f, minY);
                    _generatedGO.Add(Instantiate(shopPrefab, shopPosition, Quaternion.identity));
                    return;
                }

                if (isLeftSide)
                {
                    shopPosition = new(minX + 0.5f, Random.Range(minY, maxY));
                    _generatedGO.Add(Instantiate(shopPrefab, shopPosition, Quaternion.identity));
                    return;
                }
            }
        }

 

        private void GenerateStaircase(BSPDungeonTreeNode dungeonNode)
        {
            var roomFloors = dungeonNode.Floors;
            Vector2 randomFloorPosition = roomFloors[Random.Range(0, roomFloors.Count - 1)];
            // Add 0.5 for the game object to be instantiated in the center of the tile
            randomFloorPosition += new Vector2(0.5f, 0.5f);

            GameObject staircase = Instantiate(staircasePrefab, randomFloorPosition, Quaternion.identity);
            _generatedGO.Add(staircase);
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