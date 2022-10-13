using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratController : MonoBehaviour
{
    public float speed;
    public float range;
    public float maxDistance;

    Vector2 wayPoints;

    public GameObject coins;

    float timer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        setNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,wayPoints) < range)
        {
            setNewDestination();
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            Destroy(gameObject);
        }
    }

    void setNewDestination() // Pick a random waypoints between set distance so like if max distance is 5 then it will be from -5 to 5
    {
        wayPoints = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }

    void OnDestroy() // drop coins when destroyed 
    {
        Instantiate(coins, transform.position, coins.transform.rotation);
    }
}
