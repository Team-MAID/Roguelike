using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFactory : EnemyFactory
{
    [SerializeField]
    private GameObject ghostPrefab;

    void Start()
    {
        ghostPrefab = Resources.Load("EnemyPrefabs/Ghost") as GameObject;
    }

    public override GameObject SpawnEnemy()
    {
        if (ghostPrefab != null)
        {
            return Instantiate(ghostPrefab);
        }
        else
        {
            ghostPrefab = Resources.Load("EnemyPrefabs/Ghost") as GameObject;
            return Instantiate(ghostPrefab);
        }
    }
}
