using System.Collections;
using System.Collections.Generic;
using Items.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthBarText;
    public TextMeshProUGUI coinText;
    public Image equipedPotion;
    public Image equipedWeapon;

    public void UpdateHealthText(int t_health, int t_currentMaxHealth)
    {
        healthBarText.text = t_health.ToString() + " / " + t_currentMaxHealth.ToString();
    }
    public void UpdateCurrentHealth(int t_health)
    {
        slider.value = t_health;
    }
    public void UpdateCurrentMaxHealth(int t_currentMaxHealth)
    {
        slider.maxValue = t_currentMaxHealth;
    }

    public void UpdateCoinText(int t_coins)
    {
        coinText.text = "X " + t_coins.ToString();
    }

    public void UpdateEquippedPotion(Sprite sprite)
    {
        equipedPotion.sprite = sprite;
    }

    public void UpdateEquippedWeapon(Sprite sprite)
    {
        equipedWeapon.sprite = sprite;
    }
}
