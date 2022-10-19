using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword_Contoller : MonoBehaviour
{
    public int _HitboxDuration;
    private int _HB_Lifetime;
    private Collider2D _HB;

    // Start is called before the first frame update
    void Start()
    {
        _HB = GetComponent<Collider2D>();
        _HB_Lifetime = _HitboxDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_HB.enabled)
        {
            Debug.Log("LeftClick");
            _HB.enabled = true;
        }

       
    }

    void FixedUpdate()
    {
         if (_HB.enabled)
        {
            _HB_Lifetime--;
            if (_HB_Lifetime <= 0)
            {
                _HB.enabled = false;
                _HB_Lifetime = _HitboxDuration;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        Debug.Log("Trigger Enter");

        Destroy(_other.gameObject);
    }
}
