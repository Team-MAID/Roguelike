using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Controller : EnemyBehaviour
{
    [SerializeField]
    public int batHealth;

    [SerializeField]
    public float batSpeed;

    [SerializeField]
    public float batWanderRange;

    [SerializeField]
    public float batSwarmRange;

    [SerializeField]
    public Vector3 batScale;

    private Transform playerTrans;
    private Rigidbody2D rb;
    private Bat_Manager _batManger;

    public bool batAttacked = false;

    void Start()
    {
        setScale(batScale);
        gameObject.transform.localScale = getScale();
        rb = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        setHealth(batHealth);
        setSpeed(batSpeed);
        setDungeon();
        setWanderRange(batWanderRange);
        setAttackrange(batSwarmRange);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);

        _batManger = GetComponentInParent<Bat_Manager>();
    }

    public override void Update()
    {
        //Debug.Log(batHealth);
        if (isAlive())
        {
            // temp for testing
            if (Input.GetKeyUp(KeyCode.P))
            {
                //batAttacked = true;
                //decreaseHealth();
            }
            if (batAttacked)
            {
                if (CheckDistance(ref playerTrans, ref rb))
                {
                    followMovement(ref playerTrans, ref rb);
                }
                else
                {
                    batAttacked = false;
                }
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

    void OnCollisonEnter2D(Collider2D _other)
    {
        if (!batAttacked && (_other.CompareTag("Weapon_Player") || _other.CompareTag("Projectile_Player")))
        {
            batAttacked = true;
            _batManger.ActivateTheBats();
            FindObjectsOfType<Bat_Controller>();
        }
        if (_other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("change");
            setNewDestination();
        }
    }

}
