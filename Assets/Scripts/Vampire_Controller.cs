using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_Controller : MonoBehaviour
{
    public Bat_Manager bat_manager;
    public GameObject target;
    private Vector2 movement;
    private Rigidbody2D rb;
    public int chaseSpeed;

    public float spawnBatTimer;
    bool allowSpawnBats = true;

    public int batLimit = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) // If the Ghost has found a target, follow it.
        {
            spawnBatTimer -= Time.deltaTime;

            if(spawnBatTimer < 0)
            {
                spawnBats();
                spawnBatTimer = 5.0f;
            }
            //movement = target.transform.position - transform.position;
            //movement = movement.normalized;
            //rb.MovePosition(rb.position + movement * chaseSpeed * Time.fixedDeltaTime);
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
}
