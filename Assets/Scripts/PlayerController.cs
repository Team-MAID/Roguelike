using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rb;
    
    private Vector2 _movement;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _maxSpeed * Time.fixedDeltaTime);

        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
        Rotate(_mPos);
    }

    private void Rotate(Vector3 _mPos)
    {
        Vector3 _target;
        Vector3 _deltaRot;
        float _angle;

        _target = _mPos - this.transform.position;
        _angle = Mathf.Atan2(_target.x, _target.y) * Mathf.Rad2Deg;
        _deltaRot = new Vector3(0, 0, -_angle);

        this.transform.rotation = Quaternion.Euler(_deltaRot);
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("Input");
        Vector2 movementVector = value.Get<Vector2>();
        
        _movement.x = movementVector.x;
        _movement.y = movementVector.y;
    }
}