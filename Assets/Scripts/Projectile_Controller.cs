using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    public int _speed;
    public int _lifetime;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector2 _target;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _target.x = _mPos.x;
        _target.y = _mPos.y;

        _movement = _target - _rb.position;
        _movement.Normalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);
        _lifetime--;

        if (_lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        Debug.Log("Trigger Enter");

        Destroy(_other.gameObject);
    }
}
