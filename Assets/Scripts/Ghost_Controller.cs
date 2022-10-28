using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : MonoBehaviour
{
    public GameObject _target; // The object to follow (Player)
    private Vector2 _movement; 
    public int _speed;
    private Rigidbody2D _rb;

    public PlayerController _player;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_target != null && !_player.getHidingStatus()) // If the Ghost has found a target, follow it.
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
