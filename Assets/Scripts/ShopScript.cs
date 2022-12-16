using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    private GameObject _player;
    private GameObject _hud;
    private int _thirtyCent = 30;
    private int _fortyCent = 40;
    private int _fiftyCent = 50;
    private int _sixtyCent = 60;

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
        _player = GameObject.FindGameObjectWithTag("Player");
        _hud = GameObject.FindGameObjectWithTag("Hud");
        inventoryController = GameObject.FindWithTag("Player").GetComponent<UIInventoryController>();
        inventoryController.ShowInventory();
    }

    public void takeFromWallet(int t_cost)
    {
        _player.GetComponent<PlayerController>().coins = _player.GetComponent<PlayerController>().coins - t_cost;
        _hud.GetComponent<HUD>().UpdateCoinText(_player.GetComponent<PlayerController>().coins);
    }

    public void buyDefensePotion()
    {
        if (_player.GetComponent<PlayerController>().coins >= _fortyCent)
        {
            inventoryController.InventorySO.AddItem(defensePotion);
            takeFromWallet(_fortyCent);
        }
    }
    public void buyAttackPotion()
    {
        if (_player.GetComponent<PlayerController>().coins >= _fortyCent)
        {
            inventoryController.InventorySO.AddItem(attackPotion);
            takeFromWallet(_fortyCent);
        }
    }
    public void buyHealthPotion()
    {
        if (_player.GetComponent<PlayerController>().coins >= _fiftyCent)
        {
            inventoryController.InventorySO.AddItem(healthPotion);
            takeFromWallet(_fiftyCent);
        }
    }
    public void buyMysteryPotion()
    {
        if (_player.GetComponent<PlayerController>().coins >= _sixtyCent)
        {
            inventoryController.InventorySO.AddItem(mysteryPotion);
            takeFromWallet(_sixtyCent);
        }
    }
    public void buySpeedPotion()
    {
        if (_player.GetComponent<PlayerController>().coins >= _thirtyCent)
        {
            inventoryController.InventorySO.AddItem(speedPotion);
            takeFromWallet(_thirtyCent);
        }
    }
    public void exit()
    {
        inventoryController.HideInventory();
        Destroy(gameObject);
    }
}
