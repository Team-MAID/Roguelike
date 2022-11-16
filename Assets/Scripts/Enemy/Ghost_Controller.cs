using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : MonoBehaviour
{
    public PlayerController _player;
    private Rigidbody2D     _rb;
    public GameObject       _target; // The object to follow (Player)
    
    private Vector2 _movement; 
    private Vector2 _wayPoint;
    public float    _maxDistance;
    public float    _range;
    public int      _chaseSpeed;
    public int      _wanderSpeed;



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
            _rb.MovePosition(_rb.position + _movement * _chaseSpeed * Time.fixedDeltaTime);
        }
        else if (_target != null && _player.getHidingStatus()) // If the Ghost has found a target, follow it.
        {
            _target = null;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _wayPoint, _wanderSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _wayPoint) < _range)
            {
                setNewDestination();
            }
        }
    }

    void setNewDestination() // Pick a random waypoints between set distance so like if max distance is 5 then it will be from -5 to 5
    {
        _wayPoint = new Vector2(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance));
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player") // If the player enters the Ghost's detection radius (circle collider), set the player as the target.
        {
            _target = _other.gameObject;
        }
    }
}
