using InventorySystem;
using Items.ScriptableObjects;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Item))]
    public class EquipableItem : MonoBehaviour
    {
        private EquippedItems _equippedItems;
        private Item _item;
        
        private void Start()
        {
            _equippedItems = GameObject.FindWithTag("Player").GetComponent<EquippedItems>();
            _item = GetComponent<Item>();
        }
        
        public void Equip()
        {
            Debug.Log("Equipped " + _item.ItemData.Name);
            switch (_item.ItemData)
            {
                case PotionItemSO potionItemSO:
                    _equippedItems.EquippedPotion = potionItemSO;
                    break;
                case WeaponItemSO weaponItemSO:
                    _equippedItems.EquippedWeapon = weaponItemSO;
                    break;
            }
        }
        
        public void Unequip(GameObject user)
        {
            switch (_item.ItemData)
            {
                case PotionItemSO:
                    _equippedItems.EquippedPotion = null;
                    break;
                case WeaponItemSO:
                    _equippedItems.EquippedWeapon = null;
                    break;
            }
        }
    }
}