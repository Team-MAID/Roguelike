using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private UIInventory uiInventory;

    private Inventory _inventory;
    
    private Rigidbody2D _rb;

    private Vector2 _movement;

    int coins = 0;

    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _inventory = new Inventory();
        uiInventory.SetInventory(_inventory);

        setCoinsText();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * maxSpeed * Time.fixedDeltaTime);

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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var itemWorld = col.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            _inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
        
        if(col.gameObject.CompareTag("Coin"))
        {
            coins++;
            Destroy(col.gameObject);

            setCoinsText();
        }
    }

    void setCoinsText()
    {
        if (coinText == null) return;
        coinText.text = "Coins : " + coins.ToString();
    }
}