using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrops : MonoBehaviour
{
    public List<GameObject> itemDrops;
    public GameObject item;
    bool reacheddestination = false;

    // Start is called before the first frame update
    void Start()
    {
        itemDrops = new List<GameObject>(Resources.LoadAll<GameObject>("ItemDrops"));
        GameObject temp = itemDrops[0];
        itemDrops[0] = itemDrops[1];
        itemDrops[1] = temp;
    }

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

    void spawnCoins(int t_amount)
    {
        for (int i = 0; i < t_amount; i++)
        {
            item = Instantiate(itemDrops[0], this.transform.position, Quaternion.identity);
            item.transform.position = item.transform.position + new Vector3(Random.insideUnitCircle.x * 0.5f, Random.insideUnitCircle.y *0.5f, 0.0f);
            //randomDirection();
        }
    }

    void spawnPotion()
    {
        int randomNumber = Random.Range(1, 6);
        item = Instantiate(itemDrops[randomNumber], this.transform.position, Quaternion.identity);
        item.transform.position = item.transform.position + new Vector3(Random.insideUnitCircle.x *0.5f, Random.insideUnitCircle.y * 0.5f, 0.0f);
        //randomDirection();
    }

    void randomDirection()
    {
        while(reacheddestination != true)
        {

        }
        reacheddestination = false;
    }
}
