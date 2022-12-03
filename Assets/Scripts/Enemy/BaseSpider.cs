using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpider : EnemyBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    public float baseSpiderAttackRange;

    [SerializeField]
    public int baseSpiderHealth;

    [SerializeField]
    public float baseSpiderSpeed;

    private Transform playerPos;
    private Rigidbody2D rb;
    private Transform nestPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        setAttackrange(baseSpiderAttackRange);
        setHealth(baseSpiderHealth);
        setSpeed(baseSpiderSpeed);
        setState(EnemyBehaviourStates.Following);

        setOldPosition(this.gameObject.transform);
        nestPosition = gameObject.transform;
    }

    public override void Update()
    {
        if (isAlive())
        {
            if (CheckDistance(ref playerPos, ref rb) && getState() != EnemyBehaviourStates.Following)
            {
                setState(EnemyBehaviourStates.Following);
            }
            else if (!CheckDistance(ref playerPos, ref rb) && getState() != EnemyBehaviourStates.Idle)
            {
                setState(EnemyBehaviourStates.Idle);
            }
            if (getState() == EnemyBehaviourStates.Following)
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
}
