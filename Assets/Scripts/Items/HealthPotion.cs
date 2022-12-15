using System;
using UnityEngine;
using Items.ScriptableObjects;
using UnityEngine.Serialization;

/// <summary>
/// Item that heal the player when consumed
/// </summary>
public class HealthPotion : MonoBehaviour, IConsumable
{
    [FormerlySerializedAs("consumableItemData")] [SerializeField] private PotionItemSO potionItemData;
    private void Start()
    {
        potionItemData.ConsumingItem += Consume;
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
