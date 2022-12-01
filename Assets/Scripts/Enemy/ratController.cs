using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonGeneration.BSPGeneration;

public class ratController : MonoBehaviour
{
    public float speed;
    public float range;
    private float oldPosition = 0.0f;
    Vector2 wayPoints;

    public GameObject coins;

    BSPDungeonGenerator dungeonGenerator;

    Vector2Int ratRoomPos;
    Vector2 nextTarget;

    bool hitWall = false;
    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = FindObjectOfType<BSPDungeonGenerator>();
        castToVectorInt();
        IsInRoom();
        setNewDestination();
        oldPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextTarget) < range || hitWall)
        {
            if (hitWall)
            {
                hitWall = false;
            }
            setNewDestination();

            Debug.Log(hitWall);
        }

        if (transform.position.x > oldPosition)
        {
            gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        if (transform.position.x < oldPosition)
        {
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        oldPosition = transform.position.x;

        //Debug.Log(transform)
    }

    void setNewDestination() // Pick a random waypoints between set distance so like if max distance is 5 then it will be from -5 to 5
    {
        nextTarget = new Vector2(Random.Range(wayPoints.x - 3, wayPoints.x + 3), Random.Range(wayPoints.y - 3, wayPoints.y + 3));

        Debug.Log(nextTarget);
    }

    void OnDestroy() // drop coins when destroyed 
    {
        Instantiate(coins, transform.position, coins.transform.rotation);
    }

    void IsInRoom()
    {
        foreach (var leaf in dungeonGenerator.DungeonTree.Leafs)
        {
            if (leaf.Floors.Contains(ratRoomPos))
            {
                Debug.Log(ratRoomPos);
                wayPoints = ratRoomPos;
            }
        }
    }

    void castToVectorInt()
    {
        ratRoomPos = new Vector2Int((int)transform.position.x,(int)transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            hitWall = true;
        }
    }
}
