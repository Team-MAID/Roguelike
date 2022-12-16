using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract Class <c>EnemyFactory</c> Every Factory Derived from this must implement Spawn Enemy to be a factory
/// </summary>
public abstract class EnemyFactory : MonoBehaviour
{
    public abstract GameObject SpawnEnemy();
}
