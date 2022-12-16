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
        if (collision.CompareTag("Enemy") && collision.GetComponent<DamageEffect>().isImmuneToDamage == false)
        {                   
            collision.gameObject.GetComponent<DamageEffect>().TakeDamageEffect(0.25f);
            collision.gameObject.GetComponent<Enemy>().decreaseHealth(player.GetComponent<playerStats>().getAttackDamage());                         
        }
    }

    public int GetDamage()
    {
        return damage;
    }    
}
