using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpider : SpiderBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    public float baseSpiderAttackRange;

    [SerializeField]
    public int baseSpiderHealth;

    [SerializeField]
    public float baseSpiderSpeed;

    private Transform playerTransform;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        setAttackrange(baseSpiderAttackRange);
        setHealth(baseSpiderHealth);
        setSpeed(baseSpiderSpeed);
    }

    public override void Update()
    {
        followMovement(ref playerTransform,ref rb);
        if (!isAlive())
        {
            Debug.Log("auto death");
            Destroy(this.gameObject);
        }
    }
}
