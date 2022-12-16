using System;
using InventorySystem;
using InventorySystem.ScriptableObjects;
using InventorySystem.UI.PapyrusTheme;
using Items.Interfaces;
//using UnityEditor.UIElements;
using UnityEngine;
using Utils;

public class UIInventoryEventHandlers : MonoBehaviour
{
    [SerializeField] private UIInventoryPapyrusTheme uiInventory;
    private EquippedItems _equippedItems;

    private void Start()
    {
        uiInventory.ItemClicked += OnItemClicked;
        uiInventory.ItemRightClicked += OnItemRightClicked;

        _equippedItems = GetComponent<EquippedItems>();
    }

    //TODO: It may be a good idea to Invoke static event from here to be subscribed somwhere else instead of calling "Consume", but I'm not sure, need to think about it
    private void OnItemClicked(InventoryItem item)
    {
        // Use item
        if (item.ItemData is IEquipable equipable)
        {
            equipable.Equip();
            switch (item.ItemData)
            {
                //case WeaponItemSO weaponItem:
                    //_equippedItems.SetWeapon(weaponItem);
                    //break;
                case ConsumableItemSO consumableItemSO:
                    _equippedItems.SetConsumable(consumableItemSO);
                    break;
            }
        }

        WeaponItemSO weaponItem = item.ItemData as WeaponItemSO;
        if (weaponItem != null)
        {
            SwordController sword = GameObject.FindWithTag("Player").GetComponentInChildren<SwordController>();
            BowController bow = GameObject.FindWithTag("Player").GetComponentInChildren<BowController>();

            if (weaponItem.Name == "Bow")
            {
                if (sword != null)
                {
                    sword.Unequip(weaponItem);
                }

                if (bow == null)
                {
                    weaponItem.Equip();
                    //uiInventory.Inventory.RemoveItem(weaponItem);
                }
                else
                {
                    weaponItem.Unequip();
                    // uiInventory.Inventory.RemoveItem(weaponItem);
                }
            }
            else if (weaponItem.Name == "Sword")
            {
                if (bow != null)
                {
                    bow.Unequip(weaponItem);
                }

                if (sword == null)
                {
                    weaponItem.Equip();
                    //uiInventory.Inventory.RemoveItem(weaponItem);
                }
                else
                {
                    weaponItem.Unequip();
                    // uiInventory.Inventory.RemoveItem(weaponItem);
                }
            }
        }
    }


    private void OnItemRightClicked(InventoryItem item)
    {
        // Drop item

        Vector2 origin = transform.position;
        ItemDropEffect itemDropped = Instantiate(item.ItemData.Prefab, origin, Quaternion.identity)
            .GetComponent<ItemDropEffect>();

        Vector2 randomPos = origin + (Vector2) Direction2D.GetRandomDirection() * 1.5f;
        itemDropped.ActivateDropEffect(randomPos);

        uiInventory.Inventory.RemoveItem(item.ItemData);
    }
}