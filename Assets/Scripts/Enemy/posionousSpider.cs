using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>poisonousSpider</c> Manages poisonous spiders stats and their movement
/// </summary>
public class posionousSpider : EnemyBehaviour
{
    [SerializeField]
    public int poisonSpiderDamage;

    [SerializeField]
    public float poisonSpiderAttackRange;

    [SerializeField]
    public float poisonSpiderWanderRange;

    [SerializeField]
    public int poisonSpiderHealth;

    [SerializeField]
    public float poisonSpiderSpeed;

    [SerializeField]
    public Vector3 poisonSpiderScale;

    private Transform playerTrans;
    private Rigidbody2D rb;
    private Transform nestPosition;

    void Start()
    {
        SetDamage(poisonSpiderDamage);
        rb = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        setAttackrange(poisonSpiderAttackRange);
        setHealth(poisonSpiderHealth);
        setSpeed(poisonSpiderSpeed);
        setScale(poisonSpiderScale);
        nestPosition = gameObject.transform;
        setDungeon();
        setWanderRange(poisonSpiderWanderRange);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);
    }

    public override void Update()
    {
        if (isAlive())
        {
            if (CheckDistance(ref playerTrans, ref rb))
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
    /// Method <c>OnCollisionEnter2D</c> Sets a new destination to move to if collision with wall is detected
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
