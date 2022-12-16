using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventorySO : ScriptableObject
    {
        [field: SerializeField] public List<InventoryItem> Items { get; private set; } = new();
        public event Action InventoryChanged;

        public void AddItem(ItemSO itemData)
        {
            Debug.Log("Adding item to inventory: " + itemData);

            if (itemData.IsStackable)
            {
                InventoryItem item = Items.Find(item => item.ItemData == itemData);
                if (item == null)
                {
                    Items.Add(new InventoryItem(itemData));
                }
                else
                {
                    item.Quantity++;
                }
            }
            else if (itemData.Name == "Bow" || itemData.Name == "Sword")
            {
                InventoryItem item = Items.Find(item => item.ItemData == itemData);
                if (item == null)
                {
                    Items.Add(new InventoryItem(itemData));
                }
            }
            else
            {
                Items.Add(new InventoryItem(itemData));
            }

            InventoryChanged?.Invoke();
        }

        public InventoryItem RemoveItem(ItemSO itemData)
        {
            InventoryItem item = Items.Find(item => item.ItemData == itemData);

            if (item != null)
            {
                if (itemData.IsStackable)
                {
                    item.Quantity--;
                    if (item.Quantity == 0)
                    {
                        Items.Remove(item);
                    }
                }
                else
                {
                    Items.Remove(item);
                }
            }

            InventoryChanged?.Invoke();
            return item;
        }
    }
}