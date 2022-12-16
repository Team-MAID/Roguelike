using System;
using System.Text;
using InventorySystem.ScriptableObjects;
using Items.Interfaces;
using UnityEngine;

/// <summary>
/// Describe a Scriptable Object to create Consumable Item
/// </summary>
[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory System/Consumable Item")]
public class ConsumableItemSO : ItemSO, IConsumable, IEquipable
{
    public event Action<GameObject> ConsumingItem;
    public event Action<ItemSO> EquippingItem;
    public event Action<ItemSO> UnequippingItem;
    
    public void Consume(GameObject consumer)
    {
        ConsumingItem?.Invoke(consumer);
    }
    
    public void Equip()
    {
        EquippingItem?.Invoke(this);
    }

    public void Unequip()
    {
        UnequippingItem?.Invoke(this);
    }
}