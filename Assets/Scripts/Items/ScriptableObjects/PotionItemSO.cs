using System;
using InventorySystem.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Items.ScriptableObjects
{
    /// <summary>
    /// Describe a Scriptable Object to create Consumable Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory System/Consumable Item")]
    public class PotionItemSO : ItemSO, IConsumable, IEquipable
    {
        public event Action<GameObject> ConsumingItem;
        public static event Action<GameObject> EquippingItem;

        public void Consume(GameObject consumer)
        {
            ConsumingItem?.Invoke(consumer);
        }

        public void Equip(GameObject user)
        {
            EquippingItem?.Invoke(user);
        }

        public void Unequip(GameObject user)
        {
            throw new NotImplementedException();
        }
    }
}