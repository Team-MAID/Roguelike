using UnityEngine;

public class SwordController : WeaponItem
{
    [SerializeField] private int hitboxDuration;

    private int _slashLifetime;
    private Collider2D _hb;
    private SwordSlash _slash;
    private bool _slashTimer;
    private bool equipped;
    private void Start()
    {
        _hb = GetComponent<Collider2D>();
        _slash= transform.GetChild(0).GetComponent<SwordSlash>();

        _slashLifetime = hitboxDuration;
        _slashTimer= false;
        _hb.enabled = true;
        
        if (transform.parent != null && transform.parent.tag == "Player") { equipped = true; _hb.enabled = false; }
        else { equipped = false; }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_hb.enabled && equipped)
        {
            //Debug.Log("LeftClick");
            _slash.SwitchCollider();
            _slashTimer= true;
        }
    }

    private void FixedUpdate()
    {
        if (_slashTimer)
        {
            _slashLifetime--;
            if (_slashLifetime <= 0)
            {
                _slash.SwitchCollider();
                _slashTimer = false;
                _slashLifetime = hitboxDuration;
            }
        }
    }

    public override void Equip(ItemData data)
    {
        GameObject player = GameObject.FindWithTag("Player");
        SwordController sword = player.GetComponentInChildren<SwordController>();

        Vector3 offset = new Vector3(0.5f,0,0);

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
