using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public GameObject _target; 
    private Vector2 _movement;
    public int _speed;
    private Rigidbody2D _rb;
    private BatManager _batManger;
    public bool _enabled = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player");
        _batManger = GetComponentInParent<BatManager>();
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
        if (!_enabled && (_other.CompareTag("PlayerWeaponSword") || _other.CompareTag("PlayerProjectileBow")))
        {
            _enabled = true;
            _batManger.ActivateTheBats();
            FindObjectsOfType<BatController>();
        }
    }
}
