using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderNestFactory : EnemyFactory
{
    [SerializeField]
    private GameObject spiderNestPrefab;

    void Start()
    {
        spiderNestPrefab = Resources.Load("EnemyPrefabs/SpiderNest") as GameObject;
    }
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
