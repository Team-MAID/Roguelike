using System;
using InventorySystem.UI;
using Items.ScriptableObjects;
using UnityEngine;

public class EquippedItems : MonoBehaviour
{
    private HUD _hud;
    private UIInventoryController _inventoryController;

    private void Awake()
    {
        _hud = FindObjectOfType<HUD>();
    }

    public void Start()
    {
        _inventoryController = GetComponent<UIInventoryController>();
    }

    [SerializeField] private PotionItemSO equippedPotion;

    public PotionItemSO EquippedPotion
    {
        get => equippedPotion;
        set
        {
            if (equippedPotion != null)
            {
                _inventoryController.InventorySO.AddItem(equippedPotion);
            }
            else
            {
                _inventoryController.InventorySO.RemoveItem(equippedPotion);

                equippedPotion = value;
                if (_hud != null)
                    _hud.UpdateEquippedPotion(value.Icon);
            }
        }
    }

    [SerializeField] private WeaponItemSO equippedWeapon;

    public WeaponItemSO EquippedWeapon
    {
        get => equippedWeapon;
        set
        {
            equippedWeapon = value;
            if (_hud != null)
                _hud.UpdateEquippedWeapon(value.Icon);
        }
    }
}