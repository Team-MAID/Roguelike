using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>BatFactory</c>Bat factory loads in the bat prefab for intstaniation
/// </summary>
public class BatFactory : EnemyFactory
{
    [SerializeField]
    private GameObject batPrefab;

    void Start()
    {
        batPrefab = Resources.Load("EnemyPrefabs/Bat") as GameObject;
    }

    /// <summary>
    /// Method <c>Spawn Enemy</c> Spawns a bat enemy
    /// </summary>
    /// <returns>An instance of a bat enemy</returns>
    public override GameObject SpawnEnemy()
    {
        if (batPrefab != null)
        {
            return Instantiate(batPrefab);
        }
        else
        {
            batPrefab = Resources.Load("EnemyPrefabs/Bat") as GameObject;
            return Instantiate(batPrefab);
        }
    }
}
