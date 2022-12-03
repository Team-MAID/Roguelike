using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonGeneration.BSPGeneration;
using System.Linq;
using ExtensionMethods;

public class EnemyBehaviour : Enemy
{
    protected enum EnemyBehaviourStates
    {
        Idle = 0,
        Following = 1,
        Wandering = 2,
        Defending = 3
    };

    EnemyBehaviourStates m_enemyBehaviourState;
    private bool following = false;
    private float attackRange;
    private Vector2 movement;

    private float wanderRange;
    private float oldPosition = 0.0f;
    Vector2 wayPoints;


    BSPDungeonGenerator dungeonGenerator;

    Vector2Int currentRoomPos;
    Vector2 nextTarget;

    protected virtual void setOldPosition(Transform t_position)
    {
        oldPosition = t_position.position.x;
    }
    protected virtual void setDungeon()
    {
        dungeonGenerator = FindObjectOfType<BSPDungeonGenerator>();
    }
    protected virtual void setState(EnemyBehaviourStates t_newState)
    {
        m_enemyBehaviourState = t_newState; 
    }

    protected EnemyBehaviourStates getState()
    {
        return m_enemyBehaviourState;
    }

    public virtual void setAttackrange(float t_attackRange)
    {
        attackRange = t_attackRange;
    }
    public virtual void setWanderRange(float t_wanderRange)
    {
        wanderRange = t_wanderRange;
    }

    protected virtual void setRoomPosition(Transform t_pos)
    {
        currentRoomPos = new Vector2Int((int)t_pos.position.x, (int)t_pos.position.y);
    }

    public virtual void followMovement(ref Transform targetPos, ref Rigidbody2D targetRB)
    {
        movement = targetPos.position - transform.position;
        movement = movement.normalized;
        targetRB.MovePosition(targetRB.position + movement * speed * Time.fixedDeltaTime);

        if (movement.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-1.2f, 1.2f, 1.0f);
        }
        else if (movement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        } 
    }

    public virtual void wanderMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextTarget) < wanderRange)
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

    protected virtual void setNewDestination() // Pick a random waypoints between set distance so like if max distance is 5 then it will be from -5 to 5
    {
        foreach (var leaf in dungeonGenerator.DungeonTree.Leafs)
        {
            if (leaf.Floors.Contains(currentRoomPos))
            {
                nextTarget = leaf.Floors.GetRandomElement();
            }
        }
    }

    protected bool CheckDistance(ref Transform targetPos,ref Rigidbody2D targetRB)
    {
        // loop through enemies
        if (Vector2.Distance(targetRB.transform.position, targetPos.position) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
