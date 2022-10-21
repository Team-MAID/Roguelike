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


    // Update is called once per frame 
    void Update()
    {

        if (health > currentMaxHealth)
        {
            Debug.Log("Health Already Full");
            health = currentMaxHealth;
        }

        if (currentMaxHealth > upperHealthLimit)
        {
            Debug.Log("Max health Limit reached");
            currentMaxHealth = upperHealthLimit;
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

    public void decreaseHealth()
    {
        health--;
        Debug.Log("Health down");
    }

    public void increaseHealth()
    {
        if (health < currentMaxHealth)
        {
            health++;
            Debug.Log("Health recovered");
        }
    }

    public void increaseMaxHealth()
    {
        currentMaxHealth++;
        health = currentMaxHealth;
        Debug.Log("Max Health Increased");
    }
} 

