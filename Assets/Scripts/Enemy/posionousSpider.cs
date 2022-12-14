using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
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
            Debug.Log("auto death");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("change");
            setNewDestination();


        }
    }
}
