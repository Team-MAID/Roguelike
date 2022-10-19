using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            Destroy(_other.gameObject);
        }
    }
}
