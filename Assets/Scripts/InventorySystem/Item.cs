using System;
using UnityEngine;

namespace InventorySystem
{
    /// <summary>
    /// A stackable item that can be used with the Inventory system
    /// </summary>
    [Serializable]
    public class Item
    {
        public enum ItemType
        {
            Beef,
            RedBook,
            BlueBook,
            Candy,
            Log,
        }

        public ItemType itemType;
        public int amount;

        public Sprite GetSprite()
        {
            switch (itemType)
            {
                default:
                case ItemType.Beef: return ItemAssets.Instance.BeefSprite;
                case ItemType.RedBook: return ItemAssets.Instance.RedBookSprite;
                case ItemType.BlueBook: return ItemAssets.Instance.BlueBookSprite;
                case ItemType.Candy: return ItemAssets.Instance.CandySprite;
                case ItemType.Log: return ItemAssets.Instance.LogSprite;
            }
        }

        public bool IsStackable()
        {
            switch (itemType)
            {
                case ItemType.Beef:
                case ItemType.Candy:
                case ItemType.BlueBook:
                case ItemType.RedBook:
                    return true;
                default:
                    return false;
            }
        }
    }
}