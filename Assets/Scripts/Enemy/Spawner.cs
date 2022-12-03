using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnedEnemy;
    public int counter = 0;
    private Vector3 pos;
    private EnemyFactory ratFactory;
    private EnemyFactory ghostFactory;
    private EnemyFactory spiderFactory;
    private EnemyFactory batFactory;
    private EnemyFactory vampireFactory;
    // Start is called before the first frame update
    void Start()
    {
        ratFactory = gameObject.AddComponent<RatFactory>();
        batFactory = gameObject.AddComponent<BatFactory>();
        ghostFactory = gameObject.AddComponent<GhostFactory>();
        vampireFactory = gameObject.AddComponent<VampireFactory>();
        spiderFactory = gameObject.AddComponent<SpiderFactory>();
    }

    void Awake()
    {
        pos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            spawnNewEnemy(ratFactory);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            spawnNewEnemy(batFactory);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            spawnNewEnemy(spiderFactory);
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            spawnNewEnemy(ghostFactory);
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            spawnNewEnemy(vampireFactory);
        }
    }

    public void spawnNewEnemy(EnemyFactory t_enemyFactory)
    {
        //Debug.Log(counter);

        spawnedEnemy = t_enemyFactory.SpawnEnemy();
       // Debug.Log(counter);

       spawnedEnemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,0);
      //  Debug.Log(counter);
       // counter = counter + 1;
    }
}
