using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private int hitboxDuration;

    private int _hbLifetime;
    private Collider2D _hb;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hb = GetComponent<Collider2D>();
        _hbLifetime = hitboxDuration;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_hb.enabled)
        {
            //Debug.Log("LeftClick");
            _hb.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        // TODO: Find another way to deactivate the HitBox instead of deactivating the collider
        if (_hb.enabled)
        {
            _hbLifetime--;
            if (_hbLifetime <= 0)
            {
                _hbLifetime = hitboxDuration;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Enemy")
        {
            Destroy(_other.gameObject);
        }
    }
}