using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonGeneration.BSPGeneration;
using System.Linq;
using ExtensionMethods;

public class ratController : Enemy
{
    [SerializeField]
    public int ratHealth;

    [SerializeField]
    public float ratSpeed;


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
        setHealth(ratHealth);
        setSpeed(ratSpeed);
        dungeonGenerator = FindObjectOfType<BSPDungeonGenerator>();
        castToVectorInt();
        setNewDestination();
        oldPosition = transform.position.x;
    }

    public override void Update()
    {
        if (!isAlive())
        {
            Debug.Log("auto death");
            Destroy(this.gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextTarget) < range)
        {
            setNewDestination();
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


    }

    void setNewDestination() // Pick a random waypoints between set distance so like if max distance is 5 then it will be from -5 to 5
    {
        foreach (var leaf in dungeonGenerator.DungeonTree.Leafs)
        {
            if (leaf.Floors.Contains(ratRoomPos))
            {
               nextTarget = leaf.Floors.GetRandomElement();
            }
        }
    }

    public override void OnDestroy() // drop coins when destroyed 
    {
        Instantiate(coins, transform.position, coins.transform.rotation);
    }

    void castToVectorInt()
    {
        ratRoomPos = new Vector2Int((int)transform.position.x,(int)transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("change");
            setNewDestination();
        }
    }
}
