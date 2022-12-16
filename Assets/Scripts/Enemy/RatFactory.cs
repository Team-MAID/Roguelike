using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>RatFactory</c> A factory of type Rat. Used to Instantiate rat Enemies
/// </summary>
public class RatFactory : EnemyFactory
{
    [SerializeField]
    private GameObject ratPrefab;

    void Start()
    {
        ratPrefab = Resources.Load("EnemyPrefabs/Rat 2") as GameObject;
    }

    /// <summary>
    /// Method <c>SpawnEnemy</c> Spawns a Rat Enemy
    /// </summary>
    /// <returns>A Rat Enemy</returns>
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
