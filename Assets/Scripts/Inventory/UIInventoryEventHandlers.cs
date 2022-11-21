using System;
using InventorySystem;
using InventorySystem.ScriptableObjects;
using InventorySystem.UI.PapyrusTheme;
using UnityEngine;
using Utils;

public class UIInventoryEventHandlers : MonoBehaviour
{
    [SerializeField] private UIInventoryPapyrusTheme uiInventory;

    private void Start()
    {
        uiInventory.ItemClicked += OnItemClicked;
        uiInventory.ItemRightClicked += OnItemRightClicked;
    }

    //TODO: It may be a good idea to Invoke static event from here to be subscribed somwhere else instead of calling "Consume", but I'm not sure, need to think about it
    private void OnItemClicked(InventoryItem item)
    {
        // Use item

        ConsumableItemSO consumableItem = item.ItemData as ConsumableItemSO;
        if (consumableItem != null)
        {
            consumableItem.Consume(gameObject);
            uiInventory.Inventory.RemoveItem(consumableItem);
        }
    }


    private void OnItemRightClicked(InventoryItem item)
    {
        // Drop item

        Vector2 origin = transform.position;
        ItemDropEffect itemDropped = Instantiate(item.ItemData.Prefab, origin, Quaternion.identity)
            .GetComponent<ItemDropEffect>();

        Vector2 randomPos = origin + (Vector2) Direction2D.GetRandomDirection() * 1.5f;
        itemDropped.ActivateDropEffect(randomPos);

        uiInventory.Inventory.RemoveItem(item.ItemData);
    }
}