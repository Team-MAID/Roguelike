using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    protected float speed;
    protected Vector3 scale;

    public virtual bool isAlive()
    {
        if (health <= 0)
        {
            GetComponent<ItemDrops>().SpawnItemDrops();
            return false;
        }
        //Debug.Log("Alive");
        return true;
    }

    public virtual void setScale(Vector3 t_scale)
    {
        scale = t_scale;
    }

    public virtual Vector3 getScale()
    {
        return scale;
    }

    public virtual void setSpeed(float t_speed)
    {
        speed = t_speed;
    }

    public virtual void setHealth(int t_health)
    {
        health = t_health;
    }

    public virtual void decreaseHealth(int t_playerAttack)
    {
        health -= t_playerAttack;
    }

    public virtual void Movement()
    {
        health -= 1;
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void OnDestroy()
    {

    }

}
