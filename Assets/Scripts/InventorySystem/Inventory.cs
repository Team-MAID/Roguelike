using System;
using System.Collections.Generic;

namespace InventorySystem
{
    /// <summary>
    /// Store and manage the <see cref="Item" />s in the inventory.
    /// </summary>
    public class Inventory
    {
        public event EventHandler OnItemListChanged;
        public List<Item> ItemList { get; }

        public Inventory()
        {
            ItemList = new List<Item>();
        
            /*AddItem(new Item{ itemType = Item.ItemType.RedBook, amount = 1});
            AddItem(new Item{ itemType = Item.ItemType.Beef, amount = 1});
            AddItem(new Item{ itemType = Item.ItemType.Candy, amount = 1});*/
        }

        public void AddItem(Item item)
        {
            if (item.IsStackable())
            {
                bool itemAlreadyInInventory = false;
                foreach (Item inventoryItem in ItemList)
                {
                    if (inventoryItem.itemType == item.itemType)
                    {
                        inventoryItem.amount += item.amount;
                        itemAlreadyInInventory = true;
                    }
                }

                if (!itemAlreadyInInventory)
                {
                    ItemList.Add(item);
                }
            }
            else
            {
                ItemList.Add(item);
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}