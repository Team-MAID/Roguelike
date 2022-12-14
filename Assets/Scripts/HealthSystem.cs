using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // load game over scene
        }
    }

    public void DecreaseHealth(int t_decreaseHealth)
    {
        Debug.Log("Health Decreased");

        if (GetComponent<playerStats>().getDefense() != 0.0f)
        {
            float temp = (float)t_decreaseHealth * GetComponent<playerStats>().getDefense();
            t_decreaseHealth = (int)temp;
        }
        health -= (int)t_decreaseHealth;
        hud.UpdateCurrentHealth(health);
        hud.UpdateHealthText(health, currentMaxHealth);
    }

    public void IncreaseHealth(int t_increaseHealth)
    {
        if (health < currentMaxHealth)
        {
            health += t_increaseHealth;
            hud.UpdateCurrentHealth(health);
            hud.UpdateHealthText(health, currentMaxHealth);
        }
    }

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

