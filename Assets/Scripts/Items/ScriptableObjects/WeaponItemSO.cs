using System;
using System.Text;
using InventorySystem.ScriptableObjects;
using UnityEngine;

/// <summary>
/// Describe a Scriptable Object to create Weapon
/// </summary>
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Weapon Item")]
public class WeaponItemSO : ItemSO, IEquipable
{
    [SerializeField, Header("Quantity of damage given by this weapon")]
    private float damage;

    public event Action<GameObject> EquippingItem;
    public event Action<GameObject> UnequippingItem;

    public void Equip(GameObject user)
    {
        OnEquippingItem(user);
    }

    public void Unequip(GameObject user)
    {
        OnUnequippingItem(user);
    }

    protected virtual void OnEquippingItem(GameObject user)
    {
        EquippingItem?.Invoke(user);
    }

    protected virtual void OnUnequippingItem(GameObject user)
    {
        UnequippingItem?.Invoke(user);
    }
}