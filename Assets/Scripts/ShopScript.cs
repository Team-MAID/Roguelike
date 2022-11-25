using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public ConsumableItemSO beef;
    public ConsumableItemSO redBook;
    public ConsumableItemSO blueBook;
    public ConsumableItemSO candy;
    public ConsumableItemSO log;

    UIInventoryController inventoryController;

    private void Start()
    {
        inventoryController = GameObject.FindWithTag("Player").GetComponent<UIInventoryController>();
    }

    public void buyBeef()
    {
        inventoryController.InventorySO.AddItem(beef);
    }
    public void buyRedBook()
    {
        inventoryController.InventorySO.AddItem(redBook);
    }
    public void buyBlueBook()
    {
        inventoryController.InventorySO.AddItem(blueBook);
    }
    public void buyCandy()
    {
        inventoryController.InventorySO.AddItem(candy);
    }
    public void buyLog()
    {
        inventoryController.InventorySO.AddItem(log);
    }
    public void exit()
    {
        Destroy(gameObject);
    }
}
