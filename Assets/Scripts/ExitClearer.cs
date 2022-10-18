using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitClearer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(collision.gameObject);
            Debug.Log(collision.gameObject.transform.position);
        }
    }
}
