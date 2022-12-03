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
        return Instantiate(ratPrefab);
    }
}
