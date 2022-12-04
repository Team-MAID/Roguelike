using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFactory : EnemyFactory
{
    [SerializeField]
    private GameObject batPrefab;

    void Start()
    {
        batPrefab = Resources.Load("EnemyPrefabs/Bat") as GameObject;
    }

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
