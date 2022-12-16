using InventorySystem.ScriptableObjects;
using InventorySystem.UI;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private int                    hitboxDuration;
    [SerializeField] private WeaponItemSO           weaponItemData;
    
    private int                     slashLifetime;
    private bool                    equipped;
    private float                   angleOffset;
    private Collider2D              swordSlash;
    private Collider2D              col;
    private UIInventoryController   uiInventoryController;


    private void Start()
    {
        weaponItemData.EquippingItem += Equip;
        weaponItemData.UnequippingItem += Unequip;

        angleOffset = 0;
        uiInventoryController = GameObject.Find("Player").GetComponent<UIInventoryController>();
        swordSlash = GameObject.Find("SwordSlash").GetComponent<Collider2D>();
        col = GetComponent<Collider2D>();
        slashLifetime = hitboxDuration;
        swordSlash.enabled = false;

        if (swordSlash == null)
        {
            Debug.Log("No SwordSlash");
        }

        if (uiInventoryController == null)
        {
            Debug.Log("No Inventory Controller");
        }

        if (transform.parent != null && transform.parent.tag == "Player")
        {
            equipped = true;
            col.enabled = false;        
        }
        else { equipped = false; }
    }

    private void Update()
    {
        if (transform.parent != null && transform.parent.CompareTag("Player"))
        {
            Reposition(); 
            Rotate();   
        }

        if (Input.GetMouseButtonDown(0) && equipped && !swordSlash.enabled)
        {
            Debug.Log("Hereeee");
            swordSlash.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (swordSlash.enabled)
        {
            slashLifetime--;

            if (slashLifetime <= 0)
            {
                swordSlash.enabled = false;    
                slashLifetime = hitboxDuration;
            }
        }
    }

    private void Reposition()
    {
        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _target;
        Vector3 _parent = this.transform.parent.transform.position;
        Vector3 _newPos;

        _target = _mPos - _parent;
        _target.z = 0.0f;
        _newPos = _parent + (_target.normalized / 2);
        this.transform.position = _newPos;
    }

    private void Rotate()
    {
        Vector3 _mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _target;
        Vector3 _deltaRot;
        float _angle;

        _target = _mPos - this.transform.position;
        _angle = Mathf.Atan2(_target.x, _target.y) * Mathf.Rad2Deg;
        _deltaRot = new Vector3(0, 0, -_angle +angleOffset);

        this.transform.rotation = Quaternion.Euler(_deltaRot);
    }

    public void Equip(ItemSO item)
    {
        Debug.Log("Sword Equip Call");

        GameObject player = GameObject.FindWithTag("Player");
        SwordController sword = player.GetComponentInChildren<SwordController>();

        if (sword == null)
        {
            Debug.Log("Sword Equipped");
            Instantiate(this.weaponItemData.Prefab, player.transform.position, Quaternion.identity, player.transform);
            player.GetComponent<playerStats>().setAttackDamageWithWeapon(swordSlash.GetComponent<SwordSlashController>().GetDamage());
        }

    }

    public void Unequip(ItemSO item)
    {
        Debug.Log("Sword Unequip Call");

        GameObject player = GameObject.FindWithTag("Player");
        SwordController sword = player.GetComponentInChildren<SwordController>();

        Debug.Log("Bow Un-Equipped");
        uiInventoryController.InventorySO.AddItem(sword.weaponItemData);
        player.GetComponent<playerStats>().setAttackDamageWithWeapon(0);
        Destroy(sword.gameObject);

    }
}