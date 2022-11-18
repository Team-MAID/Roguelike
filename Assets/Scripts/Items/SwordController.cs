using UnityEngine;

public class SwordController : WeaponItem
{
    [SerializeField] private int hitboxDuration;

    private int _hbLifetime;
    private Collider2D _hb;
    private bool equipped;
    private void Start()
    {
        _hb = GetComponent<Collider2D>();
        _hbLifetime = hitboxDuration;

        if (transform.parent != null && transform.parent.tag == "Player") { equipped = true; }
        else { equipped = false; }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_hb.enabled && equipped)
        {
            //Debug.Log("LeftClick");
            _hb.enabled = true;
        }
    }

    private void FixedUpdate()
    {
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

    public override void Equip(ItemData data)
    {
        GameObject player = GameObject.FindWithTag("Player");
        SwordController sword = player.GetComponentInChildren<SwordController>();

        Vector3 offset = new Vector3(1.2f,0,0);

        if (sword == null)
        {
            Instantiate(this, player.transform.position + offset, Quaternion.identity, player.transform);
            Debug.Log("Sword Equipped");
        }
        else
        {
            Destroy(sword.gameObject);
            Debug.Log("Sword Un-Equipped");
        }
    }
}
