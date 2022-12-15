using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum VampAnimStates
{
    Idle = 0,
    Run = 1,
}
public class Vampire_Controller : EnemyBehaviour
{
    public GameObject[] spawnedEnemy;
    private EnemyFactory batFactory;
    public int counter = 0;

    [SerializeField]
    public int vampireHealth;

    [SerializeField]
    public float vampireSpeed;

    [SerializeField]
    public int vampireDamage;

    [SerializeField]
    public float vampireWanderRange;

    [SerializeField]
    public float vampireFollowRange;

    [SerializeField]
    public Vector3 vampireScale;

    [SerializeField]
    private float spawnInterval = 5.0f;

    public int limit = 5;
    private Transform playerTrans;
    private Rigidbody2D rb;

    public Bat_Manager bat_manager;
    public float spawnBatTimer;
    public HealthBar healthBar;

    public VampAnimStates vampAnimState;
    private Animator _animator;
    private static readonly int VampState = Animator.StringToHash("VampState");

    public Vector3 childScale;

    // Start is called before the first frame update
    void Start()
    {
        SetDamage(vampireDamage);
        rb = GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        setHealth(vampireHealth);
        setSpeed(vampireSpeed);
        setScale(vampireScale);
        setDungeon();
        setWanderRange(vampireWanderRange);
        setAttackrange(vampireFollowRange);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);
        batFactory = gameObject.AddComponent<BatFactory>();

        //healthBar.setMaxValue(health);
        //_animator = GetComponent<Animator>();
        //vampAnimState = VampAnimStates.Idle;
        //_animator.SetInteger(VampState, (int)vampAnimState);
    }

    /// <summary>
    /// The vampire will spawn bats until it reach the number of bats the vampire suppose to spawn
    /// </summary>
    public void spawnNewBat(EnemyFactory t_enemyfactory)
    {
        if (counter < spawnedEnemy.Length)
        {
            spawnedEnemy[counter] = t_enemyfactory.SpawnEnemy();
            spawnedEnemy[counter].transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            counter++;
        }
    }
    public void DecreaseCounter()
    {
        counter--;
    }
    // Update is called once per frame
    /// <summary>
    /// The vampire will spawn bat at interval time and follow the player if the player is in range.
    /// The vampire will fllow the player if the player is in range , if not , the vampire will wander in the room 
    /// </summary>
    public override void Update()
    {
        if (isAlive())
        {
            spawnBatTimer -= Time.deltaTime;

            Debug.Log(counter);
            if (spawnBatTimer < 0)
            {
                spawnBatTimer = spawnInterval;
                spawnNewBat(batFactory);
            }

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
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        Debug.Log("Vampire/Player Collision");
    //        collision.gameObject.GetComponent<HealthSystem>().DecreaseHealth(vampireDamage);
    //    }
    //    else if (collision.transform.CompareTag("Companion"))
    //    {
    //        Debug.Log("VampireCollision");
    //        collision.gameObject.GetComponent<CompanionController>().DecreaseHealthByDamage(vampireDamage);
    //    }
    //}
}
