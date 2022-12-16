using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Bat_Controller</c> Manages bat movement, begaviour and stats
/// </summary>
public class Bat_Controller : EnemyBehaviour
{
    [SerializeField]
    public int batDamage;

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
        SetDamage(batDamage);
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
        if (isAlive())
        {
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
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Method <c>OnCollisionEnter2D</c> Checks for collision with walls and sets a new destination to move to if detected.
    /// Activates the bats swarm beaviour if attacked.
    /// </summary>
    /// <param name="_other"></param>
    void OnCollisionEnter2D(Collision2D _other)
    {
        if (!batAttacked && (_other.gameObject.CompareTag("Weapon_Player") || _other.gameObject.CompareTag("Projectile_Player")))
        {
            batAttacked = true;
            _batManger.ActivateTheBats();
            FindObjectsOfType<Bat_Controller>();
        }
        if (_other.gameObject.CompareTag("Wall"))
        {
            setNewDestination();
        }
    }
}
