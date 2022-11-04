using System;
using System.Collections.Generic;

/// <summary>
/// Manage everything related to an inventory
/// </summary>
public class Inventory
{
    public List<InventoryItem> Items { get; } = new();

    public event Action InventoryChanged;

    public Inventory()
    {
        Item.ItemCollected += AddItem;

        ItemData.DropItem += RemoveItem;
        ConsumableItemData.ConsumeItem += RemoveItem;
    }

    /// <summary>
    /// Add an item to the list of items
    /// </summary>
    /// <remarks>
    /// If an item is stackable and already exist in the Inventory, it will increment "quantity" field rather than
    /// adding a new item to the list
    /// </remarks>
    /// <param name="item">The ScriptableObject storing the data for the item to be added</param>
    public void AddItem(ItemData item)
    {
        if (item.IsStackable)
        {
            var itemInInventory = Items.Find(x => x.Data == item);
            if (itemInInventory == null)
            {
                Items.Add(new InventoryItem(item));
            }
            else
            {
                itemInInventory.Quantity++;
            }
        }
        else
        {
            Items.Add(new InventoryItem(item));
        }

        InventoryChanged?.Invoke();
    }

    /// <summary>
    /// Remove an item from the list of items
    /// </summary>
    /// <remarks>
    /// If an item is stackable and already exist in the Inventory, it will decrement the "quantity" field rather than
    /// removing the item from the list
    /// </remarks>
    /// <param name="item">The ScriptableObject storing the data for the item to be removed</param>
    public void RemoveItem(ItemData item)
    {
        if (item.IsStackable)
        {
            var itemInInventory = Items.Find(x => x.Data == item);
            if (itemInInventory != null)
            {
                itemInInventory.Quantity--;
                if (itemInInventory.Quantity == 0)
                {
                    Items.Remove(itemInInventory);
                }
            }
        }
        else
        {
            var itemInInventory = Items.Find(x => x.Data == item);
            Items.Remove(itemInInventory);
        }

        InventoryChanged?.Invoke();
    }
}