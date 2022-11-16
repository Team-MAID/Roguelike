using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    float baseAttack;
    float baseDefense;
    float baseHealth;
    float baseSpeed;

    float attack;
    float defense;
    float health;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAttackDamage(float t_multiplier)
    {
        attack = baseAttack * t_multiplier;
    }
    public void setSpeed(float t_multiplier)
    {
        speed = baseSpeed * t_multiplier;
    }
    public void setDefense(float t_multiplier)
    {
        defense = baseDefense * t_multiplier;
    }
    //void setHealth(float t_multiplier)
    //{
    //    health = baseHealth * t_multiplier;
    //}
}
