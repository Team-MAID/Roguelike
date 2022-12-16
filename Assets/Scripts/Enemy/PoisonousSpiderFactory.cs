using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PoisonousSpiderFactory</c> A factory of poisonous spiders
/// </summary>
public class PoisonousSpiderFactory : EnemyFactory
{
    [SerializeField]
    private GameObject poisonSpiderPrefab;

    void Start()
    {
        poisonSpiderPrefab = Resources.Load("EnemyPrefabs/PoisonSpider") as GameObject;
    }

    /// <summary>
    /// Method <c>SpawnEnemy</c> Spawns a Poisonous spider.
    /// </summary>
    /// <returns>Returns an instance of a poisonous Spider</returns>
    public override GameObject SpawnEnemy()
    {
        if (poisonSpiderPrefab != null)
        {
            return Instantiate(poisonSpiderPrefab);
        }
        else
        {
            poisonSpiderPrefab = Resources.Load("EnemyPrefabs/PoisonSpider") as GameObject;
            return Instantiate(poisonSpiderPrefab);
        }
    }
}
