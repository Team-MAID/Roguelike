using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampanionController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject projectile;
    public int          maxHealth;
    public int          attackDelay;
    public int          movementSpeed;
    public int          standOffDistance;
    private int         health;
    private bool        active;
    public float        wiggleRoom;
    private float       cAttackDelay;
    private float       mVecMag;
    private Vector2     mVector;
    private Rigidbody2D rB;
    private Rigidbody2D rBP;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(6,6);

        rB = GetComponent<Rigidbody2D>();
        rBP = player.GetComponent<Rigidbody2D>();   

        cAttackDelay= attackDelay;
        health = maxHealth;
        active = true;
    }

    
    void Update()
    {
        if (player != null ) 
        {
            mVector = rBP.position - rB.position;
            mVecMag = mVector.magnitude;
            mVector.Normalize();

            if (mVecMag < standOffDistance - wiggleRoom)
            {
                rB.MovePosition(rB.position + (mVector * movementSpeed * Time.fixedDeltaTime) * -1);
            }
            else if (mVecMag > standOffDistance + wiggleRoom)
            {
                rB.MovePosition(rB.position + (mVector * movementSpeed * Time.fixedDeltaTime));
            }

        }

        if (cAttackDelay > 0)
        { 
            cAttackDelay--; 
        }

        if (Input.GetMouseButtonDown(0) && cAttackDelay <= 0 && active)
        {        
            Instantiate(projectile, this.transform.position, Quaternion.identity);
            cAttackDelay = attackDelay;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Companion Collision");

        if (other.transform.tag == "Enemy" && active)
        {
            health--;

            if (health <= 0)
            {
                active = false;  
            }
        }
    }

    void HealFull()
    {
        health = maxHealth;
    }

    void Heal(int heal)
    {
        health += heal;

        if (health > maxHealth)
        {
            health = maxHealth; 
        }
    }
}
