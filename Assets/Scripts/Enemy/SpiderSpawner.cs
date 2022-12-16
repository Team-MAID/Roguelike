using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c> SpiderSpawner</c> Spawns spiders from the nest
/// </summary>
public class SpiderSpawner : EnemyBehaviour
{
    private EnemyFactory baseSpiderFactory;
    private EnemyFactory poisonSpiderFactory;

    [SerializeField]
    public int nestHealth;

    [SerializeField]
    public Vector3 nestScale;

    [SerializeField]
    private GameObject[] spiderPrefab;

    [SerializeField]
    private GameObject[] poisonSpiderPrefab;

    [SerializeField]
    private GameObject spiderNest;

    [SerializeField]
    private float spawnInterval = 5.0f;

    public float spawnSpiderTimer = 5.0f;

    int limit = 5;
    public int counter = 0;
    public float aliveTime = 0;

    void Start()
    {
        counter = 0;
        setHealth(nestHealth);
        setScale(nestScale);
        baseSpiderFactory = gameObject.AddComponent<SpiderFactory>();
        poisonSpiderFactory = gameObject.AddComponent<PoisonousSpiderFactory>();
    }

    public override void Update()
    {
        if (isAlive())
        {
            spawnSpiderTimer -= Time.deltaTime;

            if (spawnSpiderTimer < 0)
            {
                spawnSpiderTimer = spawnInterval;
                spawnEnemy();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// method <c>spawnEnemy</c> Spawns a spider enemy. Random chance for base or poisonous spider
    /// </summary>
    private void spawnEnemy()
    {
        Debug.Log("Here");
        if (counter < limit)
        {
            int temp_randomNumber = Random.Range(0, 5);
            Debug.Log(temp_randomNumber);
            if (temp_randomNumber != 4)
            {
                spiderPrefab[counter] = baseSpiderFactory.SpawnEnemy();
                spiderPrefab[counter].transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                counter++;
            }
            else
            {
                poisonSpiderPrefab[counter] = poisonSpiderFactory.SpawnEnemy();
                poisonSpiderPrefab[counter].transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                counter++;
            }
        }
    }

    public void DecreaseCounter()
    {
        counter--;
    }
}
