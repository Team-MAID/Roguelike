using UnityEngine;

namespace InventorySystem
{
    /// <summary>
    /// Placeholder for instantiating an Item (ItemWorld) in the Scene
    /// </summary>
    /// <remarks>
    /// Game Object using this script can be placed in the Game World. Each of these game object will be replaced by a ItemWorld
    /// </remarks>
    public class ItemWorldSpawner : MonoBehaviour
    {
        [SerializeField] private Item item;

        private void Start()
        {
            ItemWorld.SpawnItemWorld(transform.position, item);
            Destroy(gameObject);
        }
    }
}
