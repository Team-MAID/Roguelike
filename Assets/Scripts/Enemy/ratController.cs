using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonGeneration.BSPGeneration;
using System.Linq;
using ExtensionMethods;

public class ratController : EnemyBehaviour
{
    [SerializeField]
    public int ratHealth;

    [SerializeField]
    public float ratSpeed;

    [SerializeField]
    public float ratWanderRange;

    // Start is called before the first frame update
    void Start()
    {
        setHealth(ratHealth);
        setSpeed(ratSpeed);
        setDungeon();
        setWanderRange(ratWanderRange);
        setState(EnemyBehaviourStates.Wandering);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);
    }

    public override void Update()
    {
        if (isAlive())
        {
            if (getState() == EnemyBehaviourStates.Wandering)
            {
                wanderMovement();
            }
        }
        else
        {
            Debug.Log("auto death");
            Destroy(this.gameObject);
        }
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
