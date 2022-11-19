using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    private Collider2D hitbox;

    private void Start()
    {
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }
    public void SwitchCollider()
    {
        hitbox.enabled = !hitbox.enabled;      
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        //Debug.Log("Slash Trigger");
        if (_other.tag == "Enemy")
        {
            Destroy(_other.gameObject);
        }
    }
}
