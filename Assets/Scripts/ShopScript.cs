using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using Items.ScriptableObjects;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public PotionItemSO defensePotion;
    public PotionItemSO attackPotion;
    public PotionItemSO healthPotion;
    public PotionItemSO mysteryPotion;
    public PotionItemSO speedPotion;

    UIInventoryController inventoryController;

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
