using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public enum PlayerAnimStates
{
    Idle = 0,
    Run= 1,
    Jumping = 2,
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;

    public PlayerAnimStates playerAnimState;

    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int State = Animator.StringToHash("State");

    private Vector2 _movement;

    int coins = 0;

    public TextMeshProUGUI coinText;

    bool isHiding = false;

    bool nearCloset = false;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerAnimState = PlayerAnimStates.Idle;
        _animator.SetInteger(State, (int)playerAnimState);

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
            _rb.MovePosition(_rb.position + _movement * maxSpeed * Time.fixedDeltaTime);
        }
        int horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        int verticalInput = (int)Input.GetAxisRaw("Vertical");

        if (horizontalInput == 0 && verticalInput == 0)
        {
            playerAnimState = PlayerAnimStates.Idle;
        }
        else
        {
            playerAnimState = PlayerAnimStates.Run;
            if (horizontalInput < 0)
            {
                gameObject.transform.localScale = new Vector3(-1.7f, 1.7f, 1.7f);
            }
            else if (horizontalInput > 0)
            {
                gameObject.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
            }

        }
        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Rotate(_mPos);
        _animator.SetInteger(State, (int)playerAnimState);

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
        if(col.gameObject.CompareTag("Coin"))
        {
            coins++;
            Destroy(col.gameObject);

            setCoinsText();
        }

        if (col.gameObject.CompareTag("Closet"))
        {
            nearCloset = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Closet"))
        {
            nearCloset = false;
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
        if (Input.GetKeyUp("h") && nearCloset)
        {
            isHiding = !isHiding;
        }
    }
}