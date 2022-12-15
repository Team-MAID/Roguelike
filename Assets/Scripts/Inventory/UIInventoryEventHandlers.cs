using InventorySystem;
using InventorySystem.UI.PapyrusTheme;
using Items;
using Items.ScriptableObjects;
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
        if (item.ItemData is IEquipable equipableItem)
        {
            equipableItem.Equip(item.ItemData.Prefab);
        }
        
        WeaponItemSO weaponItem = item.ItemData as WeaponItemSO;
        if (weaponItem != null)
        {
            SwordController sword = GameObject.FindWithTag("Player").GetComponentInChildren<SwordController>();
            BowController bow = GameObject.FindWithTag("Player").GetComponentInChildren<BowController>();

            if (weaponItem.Name == "Bow")
            {
                if (sword != null)
                {
                    sword.Unequip(sword.gameObject);
                }

                if (bow == null)
                {
                    weaponItem.Equip(gameObject);
                    //uiInventory.Inventory.RemoveItem(weaponItem);
                }
                else
                {
                    weaponItem.Unequip(gameObject);
                    // uiInventory.Inventory.RemoveItem(weaponItem);
                }
            }
            else if (weaponItem.Name == "Sword")
            {
                if (bow != null)
                {
                    bow.Unequip(bow.gameObject);
                }

                if (sword == null)
                {
                    weaponItem.Equip(gameObject);
                    //uiInventory.Inventory.RemoveItem(weaponItem);
                }
                else
                {
                    weaponItem.Unequip(gameObject);
                    // uiInventory.Inventory.RemoveItem(weaponItem);
                }
            }
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