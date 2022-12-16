using System;
using InventorySystem.ScriptableObjects;

namespace Items.Interfaces
{
    public interface IEquipable
    {
        public event Action<ItemSO> EquippingItem;
        public event Action<ItemSO> UnequippingItem;
        
        void Equip();
        void Unequip();
    }
}
