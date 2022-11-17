using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    public int health;
    public int currentMaxHealth;
    public int upperHealthLimit;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    private void Update()
    {

        if (health > currentMaxHealth)
        {
            health = currentMaxHealth;
        }

        if (currentMaxHealth > upperHealthLimit)
        {
            currentMaxHealth = upperHealthLimit;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < currentMaxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void DecreaseHealth()
    {
        health--;
    }

    public void IncreaseHealth()
    {
        if (health < currentMaxHealth)
        {
            health++;
        }
    }

    public void IncreaseMaxHealth()
    {
        if (currentMaxHealth < upperHealthLimit)
        {
            currentMaxHealth++;
        }
        health = currentMaxHealth;
    }
} 

