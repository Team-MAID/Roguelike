using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vampire_Controller : MonoBehaviour
{
    public Bat_Manager bat_manager;
    public GameObject target;
    private Vector2 movement;
    private Rigidbody2D rb;
    public int chaseSpeed;

    public float spawnBatTimer;

    public int batLimit = 0;

    int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.setMaxValue(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            if (target != null) // If the Ghost has found a target, follow it.
            {
                spawnBatTimer -= Time.deltaTime;

                if (spawnBatTimer < 0)
                {
                    spawnBats();
                    spawnBatTimer = 5.0f;
                }
                movement = target.transform.position - transform.position;
                movement = movement.normalized;
                rb.MovePosition(rb.position + movement * chaseSpeed * Time.fixedDeltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                takeDamage(5);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player") 
        {
            target = _other.gameObject;
        }
    }

   void spawnBats()
   {
        bat_manager.spawnBats();
        bat_manager.ActivateTheBats();

        Debug.Log(batLimit);
   }

    void takeDamage(int dmg)
    {
        currentHealth -= dmg;

        healthBar.setHealth(currentHealth);
    }
}
