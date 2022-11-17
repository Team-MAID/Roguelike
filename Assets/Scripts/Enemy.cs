using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int health;
    protected float speed;

    public virtual bool isAlive()
    {
        if (health <= 0)
        {
            return false;
        }
        //Debug.Log("Alive");
        return true;
    }

    public virtual void setSpeed(float t_speed)
    {
        speed = t_speed;
    }

    public virtual void setHealth(int t_health)
    {
        health = t_health;
    }

    public virtual void decreaseHealth()
    {
        health -= 1;
    }

    public virtual void movement()
    {
        health -= 1;
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }
}
