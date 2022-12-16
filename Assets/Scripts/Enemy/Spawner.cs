using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <Spawner> Takes in enemy factories and spawns differenrt types of enemies depending on the floor
/// </summary>
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


    /// <summary>
    /// Method <c>Update</c> Updates and spawns enemies depending on the floor with some randomness assigned too
    /// </summary>
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
    }

    /// <summary>
    /// Method <c>spawnNewEnemy</c> Spawns a new enemy at the spawners position
    /// </summary>
    /// <param name="t_enemyFactory"></param>
    public void spawnNewEnemy(EnemyFactory t_enemyFactory)
    {
        spawnedEnemy = t_enemyFactory.SpawnEnemy();
        spawnedEnemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,0);
    }
}
