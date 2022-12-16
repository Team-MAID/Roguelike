using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Vampire Factory</c> A Factory of Type Vampire, Used to instantiate vampires
/// </summary>
public class VampireFactory : EnemyFactory
{
    [SerializeField]
    private GameObject vampirePrefab;

    void Start()
    {
        vampirePrefab = Resources.Load("EnemyPrefabs/Vampire") as GameObject;
    }

    /// <summary>
    /// Method <c>SpawnEnemy</c> Spawns a vampire enemy
    /// </summary>
    /// <returns>A vampire enemy</returns>
    public override GameObject SpawnEnemy()
    {
        if (vampirePrefab != null)
        {
            return Instantiate(vampirePrefab);
        }
        else
        {
            vampirePrefab = Resources.Load("EnemyPrefabs/Vampire") as GameObject;
            return Instantiate(vampirePrefab);
        }
    }
}
