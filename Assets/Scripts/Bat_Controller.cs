using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Controller : MonoBehaviour
{
    public GameObject _target; 
    private Vector2 _movement;
    public int _speed;
    private Rigidbody2D _rb;
    private Bat_Manager _batManger;
    public bool _enabled = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player");
        _batManger = GetComponentInParent<Bat_Manager>();
    }

    void Update()
    {
        if (_target != null && _enabled)
        {
            _movement = _target.transform.position - transform.position;
            _movement = _movement.normalized;
            _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (!_enabled && (_other.CompareTag("Weapon_Player") || _other.CompareTag("Projectile_Player")))
        {
            _enabled = true;
            _batManger.ActivateTheBats();
            FindObjectsOfType<Bat_Controller>();
        }
    }
}
