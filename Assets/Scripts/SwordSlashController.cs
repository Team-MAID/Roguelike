using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashController : MonoBehaviour
{
    public int damage;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = 10;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.tag == "Enemy") // If the collision is with an object tagged as an enemy, destroy it. Replace with some "deal damage" fucntion later, probably. Destroy projectile as well.
            {
                collision.gameObject.GetComponent<DamageEffect>().TakeDamageEffect();
                collision.gameObject.GetComponent<Enemy>().decreaseHealth(player.GetComponent<playerStats>().getAttackDamage());
            }
        }
    }

    public int GetDamage()
    {
        return damage;
    }    
}
