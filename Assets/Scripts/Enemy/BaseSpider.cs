using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpider : EnemyBehaviour
{
    [SerializeField]
    public float baseSpiderAttackRange;

    [SerializeField]
    public int baseSpiderHealth;

    [SerializeField]
    public float baseSpiderSpeed;

    [SerializeField]
    public Vector3 baseSpiderScale;

    private Transform playerTrans;
    private Rigidbody2D rb;
    private Transform nestPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        setAttackrange(baseSpiderAttackRange);
        setHealth(baseSpiderHealth);
        setSpeed(baseSpiderSpeed);
        setScale(baseSpiderScale);
        nestPosition = gameObject.transform;
    }

    public override void Update()
    {
        if (isAlive())
        {
            if (CheckDistance(ref playerTrans, ref rb))
            {
                followMovement(ref playerTrans, ref rb);
            }
        }
        else
        {
            Debug.Log("auto death");
            Destroy(this.gameObject);
        }
    }
}
