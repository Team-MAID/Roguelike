using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        _itemList.Add(item);
    }
}