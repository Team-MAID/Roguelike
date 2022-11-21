using System;
using System.Text;
using InventorySystem.ScriptableObjects;
using UnityEngine;

/// <summary>
/// Describe a Scriptable Object to create Consumable Item
/// </summary>
[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory System/Consumable Item")]
public class ConsumableItemSO : ItemSO, IConsumable
{
    public event Action<GameObject> ConsumingItem;
    public void Consume(GameObject consumer)
    {
        OnConsumingItem(consumer);
    }

    protected virtual void OnConsumingItem(GameObject consumer)
    {
        ConsumingItem?.Invoke(consumer);
    }
}