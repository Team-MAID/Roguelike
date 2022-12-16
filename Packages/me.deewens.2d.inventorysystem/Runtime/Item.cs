using InventorySystem.Interfaces;
using InventorySystem.ScriptableObjects;
using UnityEngine;

namespace InventorySystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Item : MonoBehaviour, ICollectable
    {
        [field: SerializeField]
        public ItemSO ItemData { get; private set; }

        public void Collect(InventorySO inventorySO)
        {
            inventorySO.AddItem(ItemData);
            Destroy(gameObject);
        }
    }
}