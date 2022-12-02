using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.UI.PapyrusTheme
{
    public class UIInventoryPapyrusTheme : UIInventory
    {
        public event Action<InventoryItem> ItemClicked, ItemRightClicked;

        public override void RefreshInventorySlots()
        {
            RemoveAllItems();

            // Add each item to the UI
            foreach (var item in Inventory.Items)
            {
                // Create a new item slot from the template
                UIInventorySlotPapyrusTheme newItemSlot =
                    (UIInventorySlotPapyrusTheme) Instantiate(itemSlotPrefab, ItemSlotContainer);
                newItemSlot.SetInventoryItem(item);

                newItemSlot.ItemClicked += ItemClicked;
                newItemSlot.ItemRightClicked += ItemRightClicked;
            }
        }

        /*private void ItemClicked(UIInventorySlot item)
        {
            Debug.Log("Test");
        }

        private void ItemRightClicked(UIInventorySlot obj)
        {
            throw new System.NotImplementedException();
        }*/
    }
}