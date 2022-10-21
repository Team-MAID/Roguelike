using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [SerializeField]
    GameObject Spider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("knock Back ?");

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("knock Back ?");
            GetComponent<HealthSystem>().decreaseHealth();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collidede with enemy");
            GetComponent<HealthSystem>().decreaseHealth();

            //    if (GetComponent<HealthSystem>().health <= 0)
            //    {
            //        //Destroy(gameObject);
            //    }
        }

        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            GetComponent<HealthSystem>().increaseHealth();
            Debug.Log("collided with food");
        }

        if (collision.gameObject.CompareTag("HeartContainer"))
        {
            Destroy(collision.gameObject);
            GetComponent<HealthSystem>().increaseMaxHealth();
            Debug.Log("collided with heartContainer");
        }
    }
}
