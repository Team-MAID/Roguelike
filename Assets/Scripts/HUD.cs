using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthBarText;
    public TextMeshProUGUI coinText;

    public void UpdateHealthText(int t_health, int t_currentMaxHealth)
    {
        healthBarText.text = t_health.ToString() + " / " + t_currentMaxHealth.ToString();
    }
    public void UpdateCurrentHealth(int t_health)
    {
        Debug.Log("Hereeee");
        slider.value = t_health;
    }
    public void UpdateCurrentMaxHealth(int t_currentMaxHealth)
    {
        slider.maxValue = t_currentMaxHealth;
    }

    public void UpdateCoinText(int t_coins)
    {
        Debug.Log("COINSSS");
        coinText.text = "X " + t_coins.ToString();
    }
}
