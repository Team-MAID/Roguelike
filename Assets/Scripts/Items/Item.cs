using System;
using UnityEngine;

/// <summary>
/// Base class for every items in the game. The main script for an item must inherit from this class
/// </summary>
/// <remarks>This class inherit from MonoBehaviour</remarks>
public abstract class Item : MonoBehaviour, ICollectable
{
    public static event Action<ItemData> ItemCollected;

    [SerializeField] protected ItemData itemData;

    /// <summary>
    /// When the item is collected from the ground, its corresponding GameObject is destroyed and an event is invoked.
    /// </summary>
    public void Collect()
    {
        var dropEffect = GetComponent<ItemDropEffect>();
        if (dropEffect != null && dropEffect.IsDroppingItem) return;

        Destroy(gameObject);
        ItemCollected?.Invoke(itemData);
    }
}