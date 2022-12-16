using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>SpiderNestFactory</c> factory of Spider Nests. Spawns Spider Nests
/// </summary>
public class SpiderNestFactory : EnemyFactory
{
    [SerializeField]
    private GameObject spiderNestPrefab;

    void Start()
    {
        spiderNestPrefab = Resources.Load("EnemyPrefabs/SpiderNest") as GameObject;
    }

    /// <summary>
    /// Method <c>SpawnEnemy</c> Spawns a Spider nest
    /// </summary>
    /// <returns>returns a spider nest</returns>
    public override GameObject SpawnEnemy()
    {
        if (spiderNestPrefab != null)
        {
            return Instantiate(spiderNestPrefab);
        }
        else
        {
            spiderNestPrefab = Resources.Load("EnemyPrefabs/SpiderNest") as GameObject;
            return Instantiate(spiderNestPrefab);
        }
    }
}
