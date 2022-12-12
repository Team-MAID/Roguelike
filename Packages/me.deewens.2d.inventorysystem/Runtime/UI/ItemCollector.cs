using System;
using InventorySystem.Interfaces;
using UnityEngine;

namespace InventorySystem.UI
{
    /// <summary>
    /// Attach this to any entity that needs to be able to collect item from the ground.
    /// You need to attach a Collider or a Trigger to the GameObject for this to work.
    /// Be sure to have the UIInventoryController attached to the script too.
    /// </summary>
    [RequireComponent(typeof(UIInventoryController))]
    public class ItemCollector : MonoBehaviour
    {
        private UIInventoryController _uiInventoryController;

        private void Start()
        {
            _uiInventoryController = GetComponent<UIInventoryController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            ICollectable item = col.GetComponent<ICollectable>();
            item?.Collect(_uiInventoryController.InventorySO);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log("ItemCollector Collision");
            ICollectable item = col.gameObject.GetComponent<ICollectable>();
            item?.Collect(_uiInventoryController.InventorySO);
        }
    }
}