using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonGeneration.BSPGeneration;
using System.Linq;
using ExtensionMethods;

/// <summary>
/// Class <c>ratController</c> Manages the rat enemies and their movement
/// </summary>
public class ratController : EnemyBehaviour
{
    [SerializeField]
    public int ratDamage;

    [SerializeField]
    public int ratHealth;

    [SerializeField]
    public float ratSpeed;

    [SerializeField]
    public float ratWanderRange;

    [SerializeField]
    public Vector3 ratScale;

    void Start()
    {
        SetDamage(ratDamage);
        setHealth(ratHealth);
        setSpeed(ratSpeed);
        setScale(ratScale);
        setDungeon();
        setWanderRange(ratWanderRange);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);
    }

    public override void Update()
    {
        if (isAlive())
        {
            wanderMovement();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Method <c>OnCollisionEnter2D</c> Checks for collision with walls and sets a new destination to move to.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            setNewDestination();
        }
    }
}
