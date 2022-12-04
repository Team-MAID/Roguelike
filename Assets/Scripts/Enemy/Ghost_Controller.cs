using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : EnemyBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
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
