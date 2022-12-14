using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    //private void OnCollisionEnter2D(Collision2D _otherCollider)
    //{
    //    Debug.Log("knock Back ?");

    //    if (_otherCollider.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("knock Back ?");
    //        GetComponent<HealthSystem>().decreaseHealth();

    //        // Temporary to test decreasing health and enemy destruction. Will decrease on weapon collision later
    //       // collision.gameObject.GetComponent<SpiderBehaviour>().DecreaseHealth();
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<DamageEffect>().isImmuneToDamage == false)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                GetComponent<DamageEffect>().TakeDamageEffect(1.0f);
                GetComponent<HealthSystem>().DecreaseHealth(collision.gameObject.GetComponent<Enemy>().GetDamage());
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
        }
        //else if (collision.transform.CompareTag("Companion"))
        //{
        //    Debug.Log("VampireCollision");
        //    collision.gameObject.GetComponent<CompanionController>().DecreaseHealthByDamage(vampireDamage);
        //}
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
        }
        //else if (collision.transform.CompareTag("Companion"))
        //{
        //    Debug.Log("VampireCollision");
        //    collision.gameObject.GetComponent<CompanionController>().DecreaseHealthByDamage(vampireDamage);
        //}
    }
}
