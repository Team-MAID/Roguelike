using UnityEngine;

/// <summary>
/// Item that heal the player when consumed
/// </summary>
public class HealthPotion : ConsumableItem
{
    public override void Consume(ItemData data)
    {
        HealthSystem playerHealth = GameObject.FindWithTag("Player")?.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.IncreaseHealth();
        }
    }
}
