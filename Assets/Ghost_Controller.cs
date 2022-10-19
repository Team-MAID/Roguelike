using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : MonoBehaviour
{
    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {

        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            _target = _other.gameObject;
        }
    }
}
