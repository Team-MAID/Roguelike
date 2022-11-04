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


    private void OnTriggerEnter2D(Collider2D _otherColldier)
    {
        if (_otherColldier.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collided with enemy");
            GetComponent<HealthSystem>().DecreaseHealth();
        }

        if (_otherColldier.gameObject.CompareTag("Food"))
        {
            Destroy(_otherColldier.gameObject);
            GetComponent<HealthSystem>().IncreaseHealth();
            Debug.Log("collided with food");
        }

        if (_otherColldier.gameObject.CompareTag("HeartContainer"))
        {
            Destroy(_otherColldier.gameObject);
            GetComponent<HealthSystem>().IncreaseMaxHealth();
            Debug.Log("collided with heartContainer");
        }
    }
}
