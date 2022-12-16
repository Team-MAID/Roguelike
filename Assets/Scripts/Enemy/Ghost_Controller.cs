using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Ghost_Controller</c> Determines the ghosts movement behaviour and follow range.
/// </summary>
public class Ghost_Controller : EnemyBehaviour
{
    [SerializeField]
    public int ghostDamage;

    [SerializeField]
    public int ghostHealth;

    [SerializeField]
    public float ghostSpeed;

    [SerializeField]
    public float ghostWanderRange;

    [SerializeField]
    public float ghostFollowRange;

    [SerializeField]
    public Vector3 ghostScale;

    private Transform playerTrans;
    private Rigidbody2D rb;

    void Start()
    {
        SetDamage(ghostDamage);
        rb = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        setHealth(ghostHealth);
        setSpeed(ghostSpeed);
        setScale(ghostScale);
        setDungeon();
        setWanderRange(ghostWanderRange);
        setAttackrange(ghostFollowRange);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);
    }

    public override void Update()
    {
        if (isAlive())
        {
            if (CheckDistance(ref playerTrans, ref rb) && playerTrans.GetComponent<PlayerController>().getHidingStatus() == false)
            {
                followMovement(ref playerTrans, ref rb);
            }
            else
            {
                wanderMovement();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Method <c>OnCollisionEnter2D</c> checks for collision with walls and changes direction
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
