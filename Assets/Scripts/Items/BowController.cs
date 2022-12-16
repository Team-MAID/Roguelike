using InventorySystem;
using InventorySystem.UI;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using InventorySystem.Interfaces;
using InventorySystem.ScriptableObjects;
using Items.Interfaces;
using static UnityEngine.GraphicsBuffer;

public class BowController : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    public bool equipped;
    private float angleOffset;
    private Collider2D col;

    public int attackDelay;
    private float _AttackDelay;

    private UIInventoryController uiInventoryController;

    private Item _item;
    private WeaponItemSO _weaponItemData;

    private void Start()
    {
        _item = GetComponent<Item>();
        _weaponItemData = _item.ItemData as WeaponItemSO;
        
        _weaponItemData.EquippingItem += Equip;
        _weaponItemData.UnequippingItem += Unequip;
        
        _AttackDelay = attackDelay;
        angleOffset = -45;
        col = GetComponent<Collider2D>();
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -135));
        uiInventoryController = GameObject.Find("Player").GetComponent<UIInventoryController>();

        if (uiInventoryController == null)
        {
            Debug.Log("No Inventory Controller");
        }

        if (transform.parent != null && transform.parent.tag == "Player")
        {
            equipped = true;
            col.enabled = false;
        }
        else
        {
            equipped = false;
        }
    }

    void Update()
    {
        if (equipped)
        {
            // Reposition();
            Rotate();

            if (_AttackDelay > 0)
            {
                _AttackDelay--;
            }

            if (Input.GetMouseButtonDown(0) && _AttackDelay <= 0)
            {
                //Debug.Log("LeftClick");
                Quaternion _deltaRot = Quaternion.Euler(0, 0, -135);
                Instantiate(projectile, this.transform.position, this.transform.rotation * _deltaRot);
                _AttackDelay = attackDelay;
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
        _deltaRot = new Vector3(0, 0, -_angle + angleOffset);

        this.transform.rotation = Quaternion.Euler(_deltaRot);
    }

    public void Equip(ItemSO item)
    {
        Debug.Log("Bow Equip Call");

        GameObject player = GameObject.FindWithTag("Player");
        BowController bow = player.GetComponentInChildren<BowController>();

        if (bow == null)
        {
            Debug.Log("Bow Equipped");
            Instantiate(this._weaponItemData.Prefab, player.transform.position, Quaternion.identity, player.transform);
            player.GetComponent<playerStats>()
                .setAttackDamageWithWeapon(projectile.GetComponent<Projectile_Controller>().GetDamage());
        }
    }

    public void Unequip(ItemSO item)
    {
        Debug.Log("Bow Unequip Call");

        GameObject player = GameObject.FindWithTag("Player");
        BowController bow = player.GetComponentInChildren<BowController>();

        Debug.Log("Bow Un-Equipped");
        uiInventoryController.InventorySO.AddItem(bow._weaponItemData);
        player.GetComponent<playerStats>().setAttackDamageWithWeapon(0);
        Destroy(bow.gameObject);
    }
}