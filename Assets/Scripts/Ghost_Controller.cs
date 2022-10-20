using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : MonoBehaviour
{
    private GameObject _target; // The object to follow (Player)
    private Vector2 _movement; 
    public int _speed;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null) // If the Ghost has found a target, follow it.
        {
            _movement = _target.transform.position - transform.position;
            _movement = _movement.normalized;
            _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player") // If the player enters the Ghost's detection radius (circle collider), set the player as the target.
        {
            _target = _other.gameObject;
        }
    }
}
