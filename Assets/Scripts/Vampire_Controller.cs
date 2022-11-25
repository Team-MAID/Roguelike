using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum VampAnimStates
{
    Idle = 0,
    Run = 1,
}
public class Vampire_Controller : MonoBehaviour
{
    public Bat_Manager bat_manager;
    public GameObject target;
    private Vector2 movement;
    private Rigidbody2D rb;
    public int chaseSpeed;

    public float spawnBatTimer;

    int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    public VampAnimStates vampAnimState;
    private Animator _animator;
    private static readonly int VampState = Animator.StringToHash("VampState");

    public Vector3 childScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.setMaxValue(maxHealth);
        _animator = GetComponent<Animator>();
        vampAnimState = VampAnimStates.Idle;
        _animator.SetInteger(VampState, (int)vampAnimState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            if (target != null) // If the Vampire has found a target, follow it.
            {
                spawnBatTimer -= Time.deltaTime;

                if (spawnBatTimer < 0)
                {
                    spawnBatTimer = 5.0f;
                    spawnBats();
                }
                movement = target.transform.position - transform.position;
                movement = movement.normalized;
                rb.MovePosition(rb.position + movement * chaseSpeed * Time.fixedDeltaTime);
            }
            if (movement.x == 0 && movement.y == 0)
            {
                vampAnimState = VampAnimStates.Idle;
            }
            else
            {
                vampAnimState = VampAnimStates.Run;
                if (movement.x > 0 )
                {
                    gameObject.transform.localScale = new Vector3(-5.5f, 5.5f, 5.5f);
                }
                else if (movement.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(5.5f, 5.5f, 5.5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                takeDamage(5);
            }
            _animator.SetInteger(VampState, (int)vampAnimState);

        }
        else
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player") 
        {
            //Debug.Log("hit");
            target = _other.gameObject;
        }
    }

   void spawnBats()
   {
        bat_manager.spawnBats();
        bat_manager.updateScale(childScale);
        bat_manager.ActivateTheBats();
   }

    void takeDamage(int dmg)
    {
        currentHealth -= dmg;

        healthBar.setHealth(currentHealth);
    }
}
