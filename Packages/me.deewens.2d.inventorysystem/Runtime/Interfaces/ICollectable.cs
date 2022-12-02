using InventorySystem.ScriptableObjects;

namespace InventorySystem.Interfaces
{
    /// <summary>
    /// Items must implement this interface so item can be collected
    /// </summary>
    public interface ICollectable
    {
        /// <summary>
        /// Add item to the inventory passed in parameter
        /// </summary>
        /// <param name="inventorySO">The inventory to add Item to</param>
        void Collect(InventorySO inventorySO);
    }
}