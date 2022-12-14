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
                GetComponent<DamageEffect>().TakeDamageEffect();
                GetComponent<HealthSystem>().DecreaseHealth(20);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GetComponent<DamageEffect>().isImmuneToDamage == false)
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                Debug.Log("Vampire/Player Collision");
                GetComponent<DamageEffect>().TakeDamageEffect();
                GetComponent<HealthSystem>().DecreaseHealth(collision.GetComponent<Vampire_Controller>().GetDamage());
            }
        }
        //else if (collision.transform.CompareTag("Companion"))
        //{
        //    Debug.Log("VampireCollision");
        //    collision.gameObject.GetComponent<CompanionController>().DecreaseHealthByDamage(vampireDamage);
        //}
    }
}
