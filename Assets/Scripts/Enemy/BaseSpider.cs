using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>BaseSpider</c> Base Spider Class that defines base spider behaviour
/// </summary>
public class BaseSpider : EnemyBehaviour
{
    [SerializeField]
    public int baseSpiderDamage;

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

    void Start()
    {
        SetDamage(baseSpiderDamage);
        rb = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        setAttackrange(baseSpiderAttackRange);
        setHealth(baseSpiderHealth);
        setSpeed(baseSpiderSpeed);
        setScale(baseSpiderScale);
        nestPosition = gameObject.transform;
    }

    /// <summary>
    /// Method <c>Update</c> default update method. Spider will use follow movement
    /// </summary>
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
