using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousSpiderFactory : EnemyFactory
{
    [SerializeField]
    private GameObject poisonSpiderPrefab;

    void Start()
    {
        poisonSpiderPrefab = Resources.Load("EnemyPrefabs/PoisonSpider") as GameObject;
    }
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
