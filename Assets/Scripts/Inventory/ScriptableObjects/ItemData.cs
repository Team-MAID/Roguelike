using System;
using UnityEngine;

/// <summary>
/// Base class for every Scriptable Object considered as "Item". Every "Item" Scriptable Object should derive from this class
/// </summary>
public abstract class ItemData : ScriptableObject
{
    public static event Action<ItemData> DropItem;
    
    [SerializeField] private string displayName;
    [SerializeField] private string description;
    [SerializeField] private bool isStackable;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject prefab;
    
    public string DisplayName => displayName;
    public string Description => description;
    public bool IsStackable => isStackable;
    public Sprite Icon => icon;
    public GameObject Prefab => prefab;
    
    /// <summary>
    /// Build a text to be displayed on UI to describe the item stats
    /// </summary>
    /// <returns></returns>
    public abstract string GetItemDescriptionText();

    /// <summary>
    /// Invoke an event when dropping an item
    /// </summary>
    public void InvokeDropEvent()
    {
        DropItem?.Invoke(this);
    }
}
