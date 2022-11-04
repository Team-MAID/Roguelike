using System;
using UnityEngine;

/// <summary>
/// Store data related to a specific item stored in the Inventory
/// </summary>
[Serializable]
public class InventoryItem
{
    [SerializeField] private ItemData data;
    [SerializeField] private int quantity;

    public ItemData Data
    {
        get => data;
        set => data = value;
    }

    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }

    public InventoryItem(ItemData itemData)
    {
        data = itemData;
        Quantity++;
    }
}