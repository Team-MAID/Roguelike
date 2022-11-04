using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private const int MaxColumns = 3;
    private const int MaxRows = 4; // TODO: Check if items are overflowing in Y (implement a pagination system)
    private const float ItemSlotCellSize = 166.66f;

    private Inventory _inventory;
    public Inventory Inventory
    {
        set => _inventory = value;
    }

    // Container for every itemSlot
    private Transform _itemSlotContainer;
    // Template to be used to create a new slot in the UI
    private Transform _itemSlotTemplate;

    private void Start()
    {
        gameObject.SetActive(false);
        
        _itemSlotContainer = transform.Find("ItemSlotContainer");
        _itemSlotTemplate = _itemSlotContainer.Find("ItemSlotTemplate");
        
        RefreshInventoryItem();

        _inventory.InventoryChanged += RefreshInventoryItem;
    }

    /// <summary>
    /// Draw the inventory UI by creating each slot for each items in the Inventory
    /// </summary>
    private void RefreshInventoryItem()
    {
        RemoveAllItems();
        
        // Add each item to the UI
        var x = 0;
        var y = 0;
        
        foreach (var item in _inventory.Items)
        {
            // Create a new item slot from the template
            var newItemSlot = Instantiate(_itemSlotTemplate, _itemSlotContainer);
            InventorySlotUI slotUI = newItemSlot.gameObject.AddComponent<InventorySlotUI>();
            slotUI.Init();
            slotUI.SetPosition(new Vector2(x * ItemSlotCellSize, y * ItemSlotCellSize));
            slotUI.Item = item;
            
            x++;
            if (x >= MaxColumns)
            {
                x = 0;
                y--; // Going downward because the canvas origin is down-left
            }
        }
    }

    /// <summary>
    /// Clear the UI of all the items
    /// </summary>
    private void RemoveAllItems()
    {
        // Remove all items from the UI
        foreach (Transform itemSlot in _itemSlotContainer)
        {
            // Prevent the removal of the template
            if (itemSlot == _itemSlotTemplate) continue;
            Destroy(itemSlot.gameObject);
        }   
    }
}