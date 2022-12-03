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

    private Transform playerPos;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        setHealth(ghostHealth);
        setSpeed(ghostSpeed);
        setDungeon();
        setWanderRange(ghostWanderRange);
        setAttackrange(ghostFollowRange);
        setState(EnemyBehaviourStates.Wandering);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);
    }

    public override void Update()
    {
        if (isAlive())
        {
            if (CheckDistance(ref playerPos, ref rb) && getState() != EnemyBehaviourStates.Following)
            {
                setState(EnemyBehaviourStates.Following);
            }
            else if(!CheckDistance(ref playerPos, ref rb) && getState() != EnemyBehaviourStates.Wandering)
            {
                setState(EnemyBehaviourStates.Wandering);
            }
            if (getState() == EnemyBehaviourStates.Wandering)
            {
                wanderMovement();
            }
            else if (getState() == EnemyBehaviourStates.Following)
            {
                followMovement(ref playerPos, ref rb);
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
