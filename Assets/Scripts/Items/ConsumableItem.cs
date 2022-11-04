/// <summary>
/// Base class for every Consumable Item
/// </summary>
public abstract class ConsumableItem : Item, IConsumable
{
    /// <summary>
    /// Specific ConsumableItemData
    /// </summary>
    protected ConsumableItemData ConsumableItemData;

    public abstract void Consume(ItemData data);
}