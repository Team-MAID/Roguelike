using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posionousSpider : SpiderBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    public float poisonousSpiderAttackRange;

    [SerializeField]
    public int poisonousSpiderHealth;

    [SerializeField]
    public float poisonousSpiderSpeed;

    private Transform playerTransform;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        setAttackrange(poisonousSpiderAttackRange);
        setHealth(poisonousSpiderHealth);
        setSpeed(poisonousSpiderSpeed);
    }

    public override void Update()
    {
        followMovement(ref playerTransform, ref rb);
        if (!isAlive())
        {
            Destroy(this.gameObject);
        }
    }
}
