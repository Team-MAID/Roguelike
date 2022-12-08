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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<HealthSystem>().DecreaseHealth(20);
        }

        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            GetComponent<HealthSystem>().IncreaseHealth(20);
        }

    }
}
