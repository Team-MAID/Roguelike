using System;
using DungeonGeneration.RandomWalkGeneration;
using ExtensionMethods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGeneration
{
    public class RandomWalkLevelGenerator : LevelGenerator
    {
        private SimpleRandomWalkDungeonGenerator _RWDungeonGenerator =>
            (SimpleRandomWalkDungeonGenerator) DungeonGenerator;

        private void Start()
        {
            _RWDungeonGenerator.GenerateDungeon();

            SpawnPlayer();
            SpawnEnemies();
        }

        private void SpawnPlayer()
        {
            var randomPos = _RWDungeonGenerator.FloorPositions.GetRandomElement() + new Vector2(0.5f, 0.5f);

            playerPrefab.transform.position = randomPos;
            playerPrefab.SetActive(true);
        }

        private void SpawnEnemies()
        {
            var roomFloors = _RWDungeonGenerator.FloorPositions;

            int numberOfEnemiesToGenerate = 4; // Random.Range(1, 3);
            for (var i = 0; i < numberOfEnemiesToGenerate; ++i)
            {
                Vector2 randomFloorPosition = roomFloors.GetRandomElement();
                // Add 0.5 for the game object to be instantiated in the center of the tile
                randomFloorPosition += new Vector2(0.5f, 0.5f);

                GameObject newEnemy = Instantiate(spawnerPrefab, randomFloorPosition, Quaternion.identity);
                _generatedGO.Add(newEnemy);
            }
        }
    }
}