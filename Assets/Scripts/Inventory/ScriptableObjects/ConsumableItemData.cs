using System;
using System.Text;
using UnityEngine;

/// <summary>
/// Describe a Scriptable Object to create Consumable Item
/// </summary>
[CreateAssetMenu(fileName = "ConsumableItemData", menuName = "Items/Consumable")]
public class ConsumableItemData : ItemData
{
    public static event Action<ConsumableItemData> ConsumeItem;

    public override string GetItemDescriptionText()
    {
        var builder = new StringBuilder();
        
        // TODO: implement this method

        return builder.ToString();
    }

    /// <summary>
    /// Invoke an event when this item is consumed by something
    /// </summary>
    public void InvokeConsumeEvent()
    {
        ConsumeItem?.Invoke(this);
    }

}