using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Enemy</c> Super class all enemies inherit from.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    public int health;
    protected float speed;
    protected int damage;
    protected Vector3 scale;

    /// <summary>
    /// Method <c>isAlive</c> Checks if the enemy is alive
    /// </summary>
    /// <returns>returns true or false depending on enemy being alive or dead</returns>
    public virtual bool isAlive()
    {
        if (health <= 0)
        {
            GetComponent<ItemDrops>().SpawnItemDrops();
            return false;
        }
        return true;
    }

    /// <summary>
    /// Method <c>setScale</c> Sets the scale of the enemy
    /// </summary>
    /// <param name="t_scale"></param>
    public virtual void setScale(Vector3 t_scale)
    {
        scale = t_scale;
    }

    /// <summary>
    /// Method <c>getScale</c> returns the scale of the enemy
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 getScale()
    {
        return scale;
    }

    /// <summary>
    /// Method <c>setSpeed</c> sets the speed of the enemy
    /// </summary>
    /// <param name="t_speed"></param>
    public virtual void setSpeed(float t_speed)
    {
        speed = t_speed;
    }

    /// <summary>
    /// Method <c>Setdamage</c> sets the enemies attack damage
    /// </summary>
    /// <param name="t_damage"></param>
    public virtual void SetDamage(int t_damage)
    {
        damage = t_damage;
    }

    /// <summary>
    /// Method <c>GetDamage</c> returns enemies attack damage
    /// </summary>
    /// <returns></returns>
    public int GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// Method <c>sethealth</c> Sets an enemies health
    /// </summary>
    /// <param name="t_health"></param>
    public virtual void setHealth(int t_health)
    {
        health = t_health;
    }

    /// <summary>
    /// Method <c>decreaseHealth</c> decreases enemy health based on the player's attack stats
    /// </summary>
    /// <param name="t_playerAttack"></param>
    public virtual void decreaseHealth(int t_playerAttack)
    {
        health -= t_playerAttack;
    }

    public virtual void Movement(){}
    public virtual void Update(){}
    public virtual void OnDestroy(){}

}
