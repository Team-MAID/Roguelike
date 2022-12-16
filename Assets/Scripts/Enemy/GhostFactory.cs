using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>GhostFactory</c> A factory of type Ghost. Used to Instantiate Ghost Enemies
/// </summary>
public class GhostFactory : EnemyFactory
{
    [SerializeField]
    private GameObject ghostPrefab;

    void Start()
    {
        ghostPrefab = Resources.Load("EnemyPrefabs/Ghost") as GameObject;
    }

    /// <summary>
    /// Method <c>SpawnEnemy</c> Spawns a ghost enemy
    /// </summary>
    /// <returns>A ghost Enemy</returns>
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
