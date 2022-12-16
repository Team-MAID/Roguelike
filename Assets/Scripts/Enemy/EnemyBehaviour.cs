using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonGeneration.BSPGeneration;
using System.Linq;
using DungeonGeneration;
using DungeonGeneration.RandomWalkGeneration;
using ExtensionMethods;

/// <summary>
/// Class <c>EnemyBehaviour</c> Manages enemy behaviour patterns for enemies to utilise. main focus is enemy movement types
/// </summary>
public class EnemyBehaviour : Enemy
{
    /// <summary>
    /// Enum <c>EnemyBehaviourStates</c> The possible states for the enemy types
    /// </summary>
    protected enum EnemyBehaviourStates
    {
        Idle = 0,
        Following = 1,
        Wandering = 2,
        Defending = 3
    };

    EnemyBehaviourStates m_enemyBehaviourState;
    private float attackRange;
    private Vector2 movement;

    private float wanderRange;
    private float oldPosition = 0.0f;
    Vector2 wayPoints;


    DungeonGenerator dungeonGenerator;

    Vector2Int currentRoomPos;
    Vector2 nextTarget;

    protected virtual void setOldPosition(Transform t_position)
    {
        oldPosition = t_position.position.x;
    }

    protected virtual void setDungeon()
    {
        dungeonGenerator = FindObjectOfType<DungeonGenerator>();
    }

    /// <summary>
    /// Method <c>setEnemy</c> sets the enemies movement state
    /// </summary>
    /// <param name="t_newState"></param>
    protected virtual void setState(EnemyBehaviourStates t_newState)
    {
        m_enemyBehaviourState = t_newState;
    }

    /// <summary>
    /// Method <c>getState</c> gets the enemies current behaviour state in terms of movement
    /// </summary>
    /// <returns></returns>
    protected EnemyBehaviourStates getState()
    {
        return m_enemyBehaviourState;
    }

    /// <summary>
    /// Method <c>setAttackRange</c> sets the range in which an enemy will attack the player
    /// </summary>
    /// <param name="t_attackRange"></param>
    public virtual void setAttackrange(float t_attackRange)
    {
        attackRange = t_attackRange;
    }

    /// <summary>
    /// Method <c>setWanderRange</c> sets the range in which an enemy will wander
    /// </summary>
    /// <param name="t_wanderRange"></param>
    public virtual void setWanderRange(float t_wanderRange)
    {
        wanderRange = t_wanderRange;
    }

    /// <summary>
    /// Method <c>setroomPosition</c> sets the position of the enemy within the room
    /// </summary>
    /// <param name="t_pos"></param>
    protected virtual void setRoomPosition(Transform t_pos)
    {
        currentRoomPos = new Vector2Int((int) t_pos.position.x, (int) t_pos.position.y);
    }

    /// <summary>
    /// method <c>followMovement</c> Defines enemy follow movement / behaviour
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="targetRB"></param>
    public virtual void followMovement(ref Transform targetPos, ref Rigidbody2D targetRB)
    {
        movement = targetPos.position - transform.position;
        movement = movement.normalized;
        targetRB.MovePosition(targetRB.position + movement * speed * Time.fixedDeltaTime);

        if (movement.x > 0)
        {
            Vector3 l_scale = getScale();
            gameObject.transform.localScale = new Vector3(-l_scale.x, l_scale.y, l_scale.z);
        }
        else if (movement.x < 0)
        {
            Vector3 l_scale = getScale();
            gameObject.transform.localScale = new Vector3(l_scale.x, l_scale.y, l_scale.z);
        }
    }

    /// <summary>
    /// Method <c>wanderMovement</c> Defines enemy wander movement / behaviour
    /// </summary>
    public virtual void wanderMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextTarget) < wanderRange)
        {
            setNewDestination();
        }

        if (transform.position.x > oldPosition)
        {
            Vector3 l_scale = getScale();
            gameObject.transform.localScale = new Vector3(-l_scale.x, l_scale.y, l_scale.z);
        }

        if (transform.position.x < oldPosition)
        {
            Vector3 l_scale = getScale();
            gameObject.transform.localScale = new Vector3(l_scale.x, l_scale.y, l_scale.z);
        }

        oldPosition = transform.position.x;
    }

    /// <summary>
    /// method <c>setNewdestination</c> Sets a new destination for an enemy to move to within a room
    /// </summary>
    protected virtual void setNewDestination()
    {
        if (dungeonGenerator is BSPDungeonGenerator bspDungeonGenerator)
        {
            foreach (var leaf in bspDungeonGenerator.DungeonTree.Leafs)
            {
                if (leaf.Floors.Contains(currentRoomPos))
                {
                    nextTarget = leaf.Floors.GetRandomElement();
                }
            }
        }
        else if (dungeonGenerator is SimpleRandomWalkDungeonGenerator randomWalkDungeonGenerator)
        {
            var floorPositions = randomWalkDungeonGenerator.FloorPositions;
            if (floorPositions.Contains(currentRoomPos))
            {
                nextTarget = floorPositions.GetRandomElement();
            }
        }
    }

    /// <summary>
    /// Method <c>CheckDistance</c> Checks the distance between two target objects. (player and enemy)
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="targetRB"></param>
    /// <returns></returns>
    protected bool CheckDistance(ref Transform targetPos, ref Rigidbody2D targetRB)
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