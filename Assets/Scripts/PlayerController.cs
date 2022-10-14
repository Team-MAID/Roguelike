using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;

    private Rigidbody2D _rb;
    
    private Vector2 _movement;

    public float MaxSpeed => maxSpeed;

    int coins = 0;

    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        setCoinsText();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * (MaxSpeed * Time.fixedDeltaTime));
    }

    public void OnMove(InputValue value)
    {
        Vector2 movementVector = value.Get<Vector2>();
        
        _movement.x = movementVector.x;
        _movement.y = movementVector.y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            coins++;
            Destroy(other.gameObject);

            setCoinsText();
        }
    }

    void setCoinsText()
    {
        if (coinText == null) return;
        coinText.text = "Coins : " + coins.ToString();
    }
}