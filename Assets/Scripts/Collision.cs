using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Collision</c> Manages collision between the player and enemies
/// using variou types of collision detection
/// </summary>
public class Collision : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<DamageEffect>().isImmuneToDamage == false)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                GetComponent<DamageEffect>().TakeDamageEffect(1.0f);
                GetComponent<HealthSystem>().DecreaseHealth(collision.gameObject.GetComponent<Enemy>().GetDamage());
                if (GetComponent<HealthSystem>().health <= 0.0f)
                {
                    OnPlayerDeath?.Invoke();
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GetComponent<DamageEffect>().isImmuneToDamage == false)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                GetComponent<DamageEffect>().TakeDamageEffect(1.0f);
                GetComponent<HealthSystem>().DecreaseHealth(collision.gameObject.GetComponent<Enemy>().GetDamage());
            }
            if (GetComponent<HealthSystem>().health <= 0.0f)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<DamageEffect>().isImmuneToDamage == false)
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                GetComponent<DamageEffect>().TakeDamageEffect(1.0f);
                GetComponent<HealthSystem>().DecreaseHealth(collision.GetComponent<Enemy>().GetDamage());
            }
            if (GetComponent<HealthSystem>().health <= 0.0f)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GetComponent<DamageEffect>().isImmuneToDamage == false)
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                GetComponent<DamageEffect>().TakeDamageEffect(1.0f);
                GetComponent<HealthSystem>().DecreaseHealth(collision.GetComponent<Enemy>().GetDamage());
            }
            if (GetComponent<HealthSystem>().health <= 0.0f)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }
}
