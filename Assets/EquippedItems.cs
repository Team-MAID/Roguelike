using System.Collections;
using System.Collections.Generic;
using InventorySystem.UI;
using UnityEngine;

public class EquippedItems : MonoBehaviour
{
    [SerializeField] private WeaponItemSO _weaponItem;
    [SerializeField] private ConsumableItemSO _consumableItem;

    private HUD _hud;
    private UIInventoryController _inventoryController;

    private void Start()
    {
        _hud = FindObjectOfType<HUD>();
        _inventoryController = GetComponent<UIInventoryController>();
    }

    public void SetWeapon(WeaponItemSO weaponItemSO)
    {
        _weaponItem = weaponItemSO;

        if (_weaponItem == null)
        {
            _hud.UpdateEquippedWeapon(null);
        }
        else
        {
            _hud.UpdateEquippedWeapon(_weaponItem.Icon);
        }
    }

    public void SetConsumable(ConsumableItemSO consumableItemSO)
    {
        _consumableItem = consumableItemSO;

        if (_consumableItem == null)
        {
            _hud.UpdateEquippedPotion(null);
        }
        else
        {
            var spawned = Instantiate(_consumableItem.Prefab, Vector3.zero, Quaternion.identity);
            spawned.GetComponent<SpriteRenderer>().enabled = false;

            _hud.UpdateEquippedPotion(_consumableItem.Icon);
        }
    }

    private void OnUseEquippedPotion()
    {
        if (_consumableItem != null)
        {
            _consumableItem.Consume(gameObject);
            var leftItem = _inventoryController.InventorySO.RemoveItem(_consumableItem);
            if (leftItem.Quantity == 0)
            {
                SetConsumable(null);
            }
        }
    }
}