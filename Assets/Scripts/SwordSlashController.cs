using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashController : MonoBehaviour
{
    public int damage;

    private void Start()
    {
        damage = 10;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<EnemyHealthSystem>())
            {               
                collision.GetComponent<EnemyHealthSystem>().decreaseHealthByDamage(damage);
            }
        }
    }
}
