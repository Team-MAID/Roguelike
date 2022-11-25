using System;
using InventorySystem;
using UnityEngine;

/// <summary>
/// Item that heal the player when consumed
/// </summary>
public class HealthPotion : MonoBehaviour, IConsumable
{
    [SerializeField] private ConsumableItemSO consumableItemData;
    private void Start()
    {
        consumableItemData.ConsumingItem += Consume;
    }

    public void Consume(GameObject consumer)
    {
        HealthSystem healthComponent = consumer.GetComponent<HealthSystem>();
        if (healthComponent != null)
        {
            healthComponent.IncreaseHealth(20);
        }
    }
}
