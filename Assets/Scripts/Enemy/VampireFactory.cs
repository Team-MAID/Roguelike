using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireFactory : EnemyFactory
{
    [SerializeField]
    private GameObject vampirePrefab;

    void Start()
    {
        vampirePrefab = Resources.Load("EnemyPrefabs/Vampire") as GameObject;
    }
    public override GameObject SpawnEnemy()
    {
        return Instantiate(vampirePrefab);
    }
}
