using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>SpiderFactory</c> Factory of Spiders. Used to instantiate Spiders.
/// </summary>
public class SpiderFactory : EnemyFactory
{
    [SerializeField]
    private GameObject spiderPrefab;

    void Start()
    {
        spiderPrefab = Resources.Load("EnemyPrefabs/Spider") as GameObject;
    }

    /// <summary>
    /// Method <c>SpawnEnemy</c> Spawns a spider enemy
    /// </summary>
    /// <returns>returns an enemy spider</returns>
    public override GameObject SpawnEnemy()
    {
        if (spiderPrefab != null)
        {
            return Instantiate(spiderPrefab);
        }
        else
        {
            spiderPrefab = Resources.Load("EnemyPrefabs/Spider") as GameObject;
            return Instantiate(spiderPrefab);
        }
    }
}
