using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>HealthSystem</c> Manages the health of the player
/// </summary>
public class HealthSystem : MonoBehaviour
{

    public int health;
    public int currentMaxHealth;
    public int upperHealthLimit;
    public HUD hud;

    private void Update()
    {
        if (health > currentMaxHealth)
        {
            health = currentMaxHealth;
            hud.UpdateCurrentHealth(health);
        }

        if (currentMaxHealth > upperHealthLimit)
        {
            currentMaxHealth = upperHealthLimit;
            hud.UpdateCurrentMaxHealth(currentMaxHealth);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Method <c>DecreaseHealth</c> decreases the players health taking into account their current defense stat
    /// </summary>
    /// <param name="t_decreaseHealth"></param>
    public void DecreaseHealth(int t_decreaseHealth)
    {
        if (GetComponent<playerStats>().getDefense() == 1.0f)
        {
            t_decreaseHealth = 0;
        }
        else if (GetComponent<playerStats>().getDefense() != 0.0f)
        {
            float temp = (float)t_decreaseHealth * GetComponent<playerStats>().getDefense();
            t_decreaseHealth -= (int)temp;
        }
        health -= (int)t_decreaseHealth;
        hud.UpdateCurrentHealth(health);
        hud.UpdateHealthText(health, currentMaxHealth);
    }

    /// <summary>
    /// Method <c>IncreaseHealth</c> Increases the players health when a potion is consumed
    /// </summary>
    /// <param name="t_increaseHealth"></param>
    public void IncreaseHealth(int t_increaseHealth)
    {
        if (health < currentMaxHealth)
        {
            health += t_increaseHealth;
            hud.UpdateCurrentHealth(health);
            hud.UpdateHealthText(health, currentMaxHealth);
        }
    }

    /// <summary>
    /// Method <c>increaseMaxHealth</c> increases the player's max health within a max limit
    /// </summary>
    /// <param name="t_increaseMaxhealth"></param>
    public void IncreaseMaxHealth(int t_increaseMaxhealth)
    {
        if (currentMaxHealth < upperHealthLimit)
        {
            currentMaxHealth += t_increaseMaxhealth;
            hud.UpdateCurrentMaxHealth(currentMaxHealth);
            hud.UpdateHealthText(health, currentMaxHealth);
        }
        if (health < currentMaxHealth)
        {
            health = currentMaxHealth;
            hud.UpdateCurrentHealth(health);
            hud.UpdateHealthText(health, currentMaxHealth);
        }
    }
} 

