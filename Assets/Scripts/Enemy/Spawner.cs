using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject spawnedEnemy;
    public int counter = 0;
    private Vector3 pos;
    private EnemyFactory ratFactory;
    private EnemyFactory ghostFactory;
    private EnemyFactory spiderNestFactory;
    private EnemyFactory batFactory;
    private EnemyFactory vampireFactory;
    Scene currentScene;
    private bool spawning;

    // Start is called before the first frame update
    void Start()
    {
        ratFactory = gameObject.AddComponent<RatFactory>();
        batFactory = gameObject.AddComponent<BatFactory>();
        ghostFactory = gameObject.AddComponent<GhostFactory>();
        vampireFactory = gameObject.AddComponent<VampireFactory>();
        spiderNestFactory = gameObject.AddComponent<SpiderNestFactory>();
        currentScene = SceneManager.GetActiveScene();
        spawning = true;
    }

    void Awake()
    {
        pos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {


        // Check if the name of the current Active Scene is your first Scene.
        if (currentScene.name == "FloorOne" && spawning == true)
        {
            spawnNewEnemy(ratFactory);
            spawning = false;
        }
        else if (currentScene.name == "FloorTwo" && spawning == true)
        {
            int temp_randomNumber = Random.Range(0, 4);
            Debug.Log(temp_randomNumber);
            if (temp_randomNumber != 0)
            {
                spawnNewEnemy(ratFactory);
            }
            else
            {
                spawnNewEnemy(spiderNestFactory);
            }
            spawning = false;
        }
        else if (currentScene.name == "FloorThree" && spawning == true)
        {
            int temp_randomNumber = Random.Range(0, 4);
            Debug.Log(temp_randomNumber);
            if (temp_randomNumber == 1)
            {
                spawnNewEnemy(ghostFactory);
            }
            else if (temp_randomNumber == 2)
            {
                spawnNewEnemy(spiderNestFactory);
            }
            else if (temp_randomNumber == 3)
            {
                spawnNewEnemy(ratFactory);
            }
            else if (temp_randomNumber == 4)
            {
                spawnNewEnemy(batFactory);
            }
            spawning = false;
        }
        else if (currentScene.name == "FloorFour" && spawning == true)
        {
            spawnNewEnemy(vampireFactory);
            spawning = false;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(4);
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
