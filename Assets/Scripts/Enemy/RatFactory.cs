using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFactory : EnemyFactory
{
    [SerializeField]
    private GameObject ratPrefab;

    void Start()
    {
        ratPrefab = Resources.Load("EnemyPrefabs/Rat 2") as GameObject;

    }

    public override GameObject SpawnEnemy()
    {
        if (ratPrefab != null)
        {
            return Instantiate(ratPrefab);
        }
        else
        {
            ratPrefab = Resources.Load("EnemyPrefabs/Rat 2") as GameObject;
            return Instantiate(ratPrefab);
        }
    }
}
