using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector2 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorld, position, Quaternion.identity);
        
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }
    
    private Item _item;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return _item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
