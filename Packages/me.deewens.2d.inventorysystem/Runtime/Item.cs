using InventorySystem.Interfaces;
using InventorySystem.ScriptableObjects;
using UnityEngine;

namespace InventorySystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Item : MonoBehaviour, ICollectable
    {
        [SerializeField] private ItemSO itemData;
        
        public void Collect(InventorySO inventorySO)
        {
            inventorySO.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}