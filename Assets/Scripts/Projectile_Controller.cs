using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    public int _speed; // The projectil's speed.
    public int _lifetime; // The projectile's liftetime, gets destroyed at 0.
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector2 _target; // The point toward which the projectile will be shot.

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get the mouse position relative to the game world.
        _target.x = _mPos.x; // Assign the mouse positon to local vector.
        _target.y = _mPos.y; // Assign the mouse positon to local vector.

        _movement = _target - _rb.position; // Establish the movement vector.
        _movement.Normalize(); // Normalise the movement vector.
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime); //  Movement.
        _lifetime--; // Decrement the projectile's lifetime.

        if (_lifetime <= 0) // When lifetime hits 0, destroy the projectile.
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        //Debug.Log("Trigger Enter");

        if (_other.tag == "Enemy" ) // If the collision is with an object tagged as an enemy, destroy it. Replace with some "deal damage" fucntion later, probably. Destroy projectile as well.
        {

            if (_other.gameObject.GetComponent<EnemyHealthSystem>())
            {
                Debug.Log("Enemy Health System Called");
                _other.gameObject.GetComponent<EnemyHealthSystem>().decreaseHealth();
            }
            else
            {
                Debug.Log("Object Destroyed by Drojectile");
                Destroy(_other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
