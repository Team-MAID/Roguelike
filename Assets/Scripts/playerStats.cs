using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    float baseAttack;
    float baseDefense;
    float baseHealth;
    float baseSpeed;

    float weaponDamage;
    float armourDefense;

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
        attack = baseAttack * t_multiplier + weaponDamage;
    }
    public void setSpeed(float t_multiplier)
    {
        Debug.Log(speed);
        speed = baseSpeed * t_multiplier;
        Debug.Log(speed);
    }
    public void setDefense(float t_multiplier)
    {
        defense = baseDefense * t_multiplier + armourDefense;
    }
    public void setAttackDamage()
    {
        attack = baseAttack + weaponDamage;
    }
    public void setSpeed()
    {
        speed = baseSpeed;
    }
    public void setDefense()
    {
        defense = baseDefense + armourDefense;
    }
    //void setHealth(float t_multiplier)
    //{
    //    health = baseHealth * t_multiplier;
    //}
}
