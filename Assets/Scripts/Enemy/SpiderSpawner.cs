using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] spiderPrefab;

    [SerializeField]
    private GameObject[] poisonSpiderPrefab;

    [SerializeField]
    private GameObject spiderNest;

    [SerializeField]
    private float spawnInterval = 5.0f;

    int limit = 5;
    public int counter = 0;
    public float aliveTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        StartCoroutine(spawnEnemy(spawnInterval));
    }

    private IEnumerator spawnEnemy(float interval)
    {
        if (counter < limit)
        {
            int temp_randomNumber = Random.Range(0, 3);
            Debug.Log(temp_randomNumber);
            if (temp_randomNumber != 2)
            {
                spiderPrefab[counter] = Instantiate(spiderPrefab[counter],
                    new Vector3(spiderNest.transform.position.x, spiderNest.transform.position.y, 0), Quaternion.identity);
                counter += 1;
            }
            else
            {
                poisonSpiderPrefab[counter] = Instantiate(poisonSpiderPrefab[counter],
                    new Vector3(spiderNest.transform.position.x, spiderNest.transform.position.y, 0), Quaternion.identity);
                counter += 1;
            }
        }
        yield return new WaitForSeconds(interval);
        //GameObject newEnemy = Instantiate(enemy, new Vector3(spiderNest.transform.position.x, spiderNest.transform.position.y, 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval));
    }

    public void DecreaseCounter()
    {
        counter--;
    }
}
