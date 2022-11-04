using UnityEngine;
using Utils;

/// <summary>
/// Manage the inventory of the player, instantiate the UI and the Inventory itself.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = new Inventory();
        if (inventoryUI != null)
        {
            inventoryUI.Inventory = _inventory;
        }

        ItemData.DropItem += DropItem;
    }

    private void OnOpenInventory()
    {
        inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
    }

    private void DropItem(ItemData itemData)
    {
        Vector2 origin = transform.position;
        ItemDropEffect itemDropped = Instantiate(itemData.Prefab, origin, Quaternion.identity).GetComponent<ItemDropEffect>();
        
        Vector2 randomPos = origin + (Vector2) Direction2D.GetRandomDirection() * 1.5f;
        itemDropped.ActivateDropEffect(randomPos);
    }
}