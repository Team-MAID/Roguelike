public class Item
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        Coin,
        Medkit
    }

    public ItemType itemType;
    public int amount;
}
