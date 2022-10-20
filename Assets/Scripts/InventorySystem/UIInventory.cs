using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    /// <summary>
    /// Manage the UI of the <see cref="Inventory" />, as well as any event occuring on the items
    /// </summary>
    public class UIInventory : MonoBehaviour
    {
        private const int MaxColumns = 3;
        private const int MaxRows = 4; // TODO: Check if items are overflowing in Y (implement a pagination system)
        private const float ItemSlotCellSize = 166.66f;
        
        private Inventory _inventory;

        private Transform _itemSlotContainer;
        private Transform _itemSlotTemplate;

        private void Awake()
        {
            _itemSlotContainer = transform.Find("ItemSlotContainer");
            _itemSlotTemplate = _itemSlotContainer.Find("ItemSlotTemplate");
        }

        private void RefreshInventoryItems()
        {
            // Remove all the items from the UI
            foreach (Transform child in _itemSlotContainer)
            {
                if (child == _itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
            
            // Add each item to the UI
            int x = 0;
            int y = 0;
            foreach (Item item in _inventory.ItemList)
            {
                RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector2(x * ItemSlotCellSize, y * ItemSlotCellSize);
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();
                
                x++;
                if (x > MaxColumns)
                {
                    x = 0;
                    y++;
                }
            }
        }

        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;

            _inventory.OnItemListChanged += (sender, args) => RefreshInventoryItems();
                
            RefreshInventoryItems();
        }
    }
}
