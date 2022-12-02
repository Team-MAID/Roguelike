using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.UI
{
    public abstract class UIInventorySlot : MonoBehaviour
    {
        protected InventoryItem Item;

        public virtual void SetInventoryItem(InventoryItem item)
        {
            Item = item;
        }
    }
}