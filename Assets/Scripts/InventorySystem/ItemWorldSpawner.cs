using UnityEngine;

namespace InventorySystem
{
    public class ItemWorldSpawner : MonoBehaviour
    {
        [SerializeField] private Item item;

        private void Awake()
        {
            ItemWorld.SpawnItemWorld(transform.position, item);
            Destroy(gameObject);
        }
    }
}
