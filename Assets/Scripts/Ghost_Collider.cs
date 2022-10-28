using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Collider : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
       
    }

    void OnDestroy()// When this gets destroyed, destroy the parent gameObject (the actual Ghost object).
    {
        Destroy(transform.parent.gameObject);
    }
}
