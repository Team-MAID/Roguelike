using System;
using System.Text;
using InventorySystem.ScriptableObjects;
using Items.Interfaces;
using UnityEngine;

/// <summary>
/// Describe a Scriptable Object to create Weapon
/// </summary>
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Weapon Item")]
public class WeaponItemSO : ItemSO, IEquipable
{
    [SerializeField, Header("Quantity of damage given by this weapon")]
    private float damage;

    public event Action<ItemSO> EquippingItem;
    public event Action<ItemSO> UnequippingItem;

    public void Equip()
    {
        EquippingItem?.Invoke(this);
    }

    public void Unequip()
    {
        UnequippingItem?.Invoke(this);
    }
}