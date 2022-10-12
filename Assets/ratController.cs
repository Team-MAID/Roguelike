using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratController : MonoBehaviour
{
    public float speed;
    public float range;
    public float maxDistance;

    Vector2 wayPoints;
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
    }

    void setNewDestination()
    {
        wayPoints = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }
}
