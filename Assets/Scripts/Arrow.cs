using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject player;
    public int _damage;
    public int _speed; // The projectil's speed.
    public int _lifetime; // The projectile's liftetime, gets destroyed at 0.
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector2 _target; // The point toward which the projectile will be shot.
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreLayerCollision(6,6);

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
        if (_other.gameObject.CompareTag("Enemy") && _other.GetComponent<DamageEffect>().isImmuneToDamage == false)
        {                      
            _other.gameObject.GetComponent<DamageEffect>().TakeDamageEffect(0.25f);
            _other.gameObject.GetComponent<Enemy>().decreaseHealth(player.GetComponent<playerStats>().getAttackDamage());
            _lifetime = 0;
            Destroy(this.gameObject);            
        }
    }

    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.CompareTag("Enemy") && _other.gameObject.GetComponent<DamageEffect>().isImmuneToDamage == false)
        {              
            _other.gameObject.GetComponent<DamageEffect>().TakeDamageEffect(0.25f);
            _other.gameObject.GetComponent<Enemy>().decreaseHealth(player.GetComponent<playerStats>().getAttackDamage());
            _lifetime = 0;
            Destroy(this.gameObject);
        }
    }

    public int GetDamage()
    {
        return _damage;
    }
}
