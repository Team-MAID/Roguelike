using System;
using System.Collections.Generic;
using InventorySystem.ScriptableObjects;
using UnityEngine;

namespace InventorySystem.UI
{
    public abstract class UIInventory : MonoBehaviour
    {
        [SerializeField] [Tooltip("Template Prefab to be cloned when drawing a new slot for an item in the inventory")]
        protected UIInventorySlot itemSlotPrefab;

        /// <summary>
        /// Container Game Object where every item slots will be instantiated
        /// </summary>
        protected Transform ItemSlotContainer;
        
        private InventorySO _inventory;
        public InventorySO Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                _inventory.InventoryChanged += RefreshInventorySlots;
                RefreshInventorySlots();
            }
        }

        private void Awake()
        {
            ItemSlotContainer = transform.Find("ItemSlotContainer");
            if (ItemSlotContainer == null)
            {
                throw new InventoryUIElementNotFoundException(
                    "ItemSlotContainer GameObject is missing inside UIInventoryContainer");
            }
        }

        public abstract void RefreshInventorySlots();

        /// <summary>
        /// Clear the UI of all its items
        /// </summary>
        protected void RemoveAllItems()
        {
            // Remove all items from the UI
            foreach (Transform itemSlot in ItemSlotContainer)
            {
                Destroy(itemSlot.gameObject);
            }
        }

        public void Show()
        {
            Time.timeScale = 0f;
            GetComponent<CanvasGroup>().alpha = 1;
        }

        public void Hide()
        {
            Time.timeScale = 1f;
            GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}