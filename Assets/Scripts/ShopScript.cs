using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public ConsumableItemSO defensePotion;
    public ConsumableItemSO attackPotion;
    public ConsumableItemSO healthPotion;
    public ConsumableItemSO mysteryPotion;
    public ConsumableItemSO speedPotion;

    UIInventoryController inventoryController;

    /// <summary>
    /// Opening the shop will automatically open the inventory for the player
    /// </summary>
    private void Start()
    {
        inventoryController = GameObject.FindWithTag("Player").GetComponent<UIInventoryController>();
        inventoryController.ShowInventory();
    }

    public void buyDefensePotion()
    {
        inventoryController.InventorySO.AddItem(defensePotion);
    }
    public void buyAttackPotion()
    {
        inventoryController.InventorySO.AddItem(attackPotion);
    }
    public void buyHealthPotion()
    {
        inventoryController.InventorySO.AddItem(healthPotion);
    }
    public void buyMysteryPotion()
    {
        inventoryController.InventorySO.AddItem(mysteryPotion);
    }
    public void buySpeedPotion()
    {
        inventoryController.InventorySO.AddItem(speedPotion);
    }
    public void exit()
    {
        inventoryController.HideInventory();
        Destroy(gameObject);
    }
}
