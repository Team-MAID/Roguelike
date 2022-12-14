using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonGeneration.BSPGeneration;
using System.Linq;
using ExtensionMethods;

public class ratController : EnemyBehaviour
{
    [SerializeField]
    public int ratDamage;

    [SerializeField]
    public int ratHealth;

    [SerializeField]
    public float ratSpeed;

    [SerializeField]
    public float ratWanderRange;

    [SerializeField]
    public Vector3 ratScale;

    // Start is called before the first frame update
    void Start()
    {
        SetDamage(ratDamage);
        setHealth(ratHealth);
        setSpeed(ratSpeed);
        setScale(ratScale);
        setDungeon();
        setWanderRange(ratWanderRange);
        setRoomPosition(this.gameObject.transform);
        setNewDestination();
        setOldPosition(this.gameObject.transform);
    }

    public override void Update()
    {
        if (isAlive())
        {
            wanderMovement();
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
