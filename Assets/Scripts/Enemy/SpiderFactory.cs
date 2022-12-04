using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFactory : EnemyFactory
{
    [SerializeField]
    private GameObject spiderPrefab;

    void Start()
    {
        spiderPrefab = Resources.Load("EnemyPrefabs/Spider") as GameObject;
    }

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
