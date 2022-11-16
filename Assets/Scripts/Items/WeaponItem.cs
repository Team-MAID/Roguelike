/// <summary>
/// Base class for every Weapon
/// </summary>
public abstract class WeaponItem : Item, IEquipable
{
    /// <summary>
    /// Specific WeaponItemData
    /// </summary>
    protected WeaponItemData WeaponItemData;

    public abstract void Equip(ItemData data);
}