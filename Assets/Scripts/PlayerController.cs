using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rb;
    
    private Vector2 _movement;

    public float MaxSpeed => _maxSpeed;

    int coins = 0;

    public TextMeshProUGUI coinText;

    bool isHiding = false;

    bool nearCloset = false;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        setCoinsText();
    }
    
    private void Update()
    {
        hidingKeyPress();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!isHiding)
        {
            _rb.MovePosition(_rb.position + _movement * _maxSpeed * Time.fixedDeltaTime);
        }

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
        Vector2 movementVector = value.Get<Vector2>();
        
        _movement.x = movementVector.x;
        _movement.y = movementVector.y;

        isHiding = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            coins++;
            Destroy(other.gameObject);

            setCoinsText();
        }

        if (other.gameObject.CompareTag("Closet"))
        {
            nearCloset = true;
        }
    }

    void setCoinsText()
    {
        if (coinText == null) return;
        coinText.text = "Coins : " + coins.ToString();
    }

    public bool getHidingStatus()
    {
        return isHiding;
    }

    void hidingKeyPress()
    {
        if(Input.GetKey("h") && nearCloset)
        {
            isHiding = true;
        }
    }
}