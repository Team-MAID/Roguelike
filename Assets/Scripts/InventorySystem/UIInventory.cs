using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class UIInventory : MonoBehaviour
    {
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
            // Delete all the items from the UI
            foreach (Transform child in _itemSlotContainer)
            {
                if (child == _itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
            
            // Add each item to the UI
            int x = 0;
            int y = 0;
            float itemSlotCellSize = 100f;
            foreach (Item item in _inventory.ItemList)
            {
                RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();
                
                x++;
                if (x > 4)
                {
                    x = 0;
                    y++;
                }
            }
        }
        
        public Inventory Inventory
        {
            private get { return _inventory; }
            set
            {
                _inventory = value;

                _inventory.OnItemListChanged += (sender, args) => RefreshInventoryItems();
                
                RefreshInventoryItems();
            }
        }
    }
}
