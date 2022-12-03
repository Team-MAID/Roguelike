using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFactory : EnemyFactory
{
    [SerializeField]
    private GameObject spiderPrefab;

    void Start()
    {
        spiderPrefab = Resources.Load("EnemyPrefabs/PoisonSpider") as GameObject;
    }

    public override GameObject SpawnEnemy()
    {
        return Instantiate(spiderPrefab);
    }
}
