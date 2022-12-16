using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ItemDrops</c> Creates a list of items that can Drop from enemies and spawns them when called
/// </summary>
public class ItemDrops : MonoBehaviour
{
    public List<GameObject> itemDrops;
    public GameObject item;
    bool reacheddestination = false;

    void Start()
    {
        itemDrops = new List<GameObject>(Resources.LoadAll<GameObject>("ItemDrops"));
        GameObject temp = itemDrops[0];
        itemDrops[0] = itemDrops[1];
        itemDrops[1] = temp;
    }

    /// <summary>
    /// Method <c>SpawnItemDrops</c> Spawns coins and potions with a random number used for spawn rates
    /// </summary>
    public void SpawnItemDrops()
    {
        int randomNumber = Random.Range(0,8);
        if(randomNumber <= 2)
        {
            spawnCoins(2);
        }
        else if (randomNumber <= 4)
        {
            spawnCoins(randomNumber);

        }
        else if (randomNumber >= 5)
        {
            spawnCoins(2);
            spawnPotion();
        }
    }

    /// <summary>
    /// Method <c>spawnCoins</c> Instantiates a number of coins depending on the amount requested at a random position
    /// within a unit circle where an enemy has died.
    /// </summary>
    /// <param name="t_amount"></param>
    void spawnCoins(int t_amount)
    {
        for (int i = 0; i < t_amount; i++)
        {
            item = Instantiate(itemDrops[0], this.transform.position, Quaternion.identity);
            item.transform.position = item.transform.position + new Vector3(Random.insideUnitCircle.x * 0.5f, Random.insideUnitCircle.y *0.5f, 0.0f);
        }
    }

    /// <summary>
    /// Method <c>spawnPotion</c> Instantiates a random potion
    /// within a unit circle where an enemy has died.
    /// </summary>
    void spawnPotion()
    {
        int randomNumber = Random.Range(1, 6);
        item = Instantiate(itemDrops[randomNumber], this.transform.position, Quaternion.identity);
        item.transform.position = item.transform.position + new Vector3(Random.insideUnitCircle.x *0.5f, Random.insideUnitCircle.y * 0.5f, 0.0f);
    }
}
