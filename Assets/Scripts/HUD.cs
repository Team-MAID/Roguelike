using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

/// <summary>
/// Class <c>HUD</c> Manages HUD elements and updates them
/// </summary>
public class HUD : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthBarText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI _playerAttackText;
    public TextMeshProUGUI _playerDefenseText;
    public TextMeshProUGUI _playerSpeedText;
    
    public Image equippedPotion;
    public Image equippedWeapon;

    [SerializeField] private Sprite emptySlotSprite;

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

    public void UpdateAttackText(int t_attack)
    {
        _playerAttackText.text = t_attack.ToString();
    }
    public void UpdateDefenseText(int t_defense)
    {
        _playerDefenseText.text = t_defense.ToString() + "%";
    }

    public void UpdateSpeedText(float t_speed)
    {
        _playerSpeedText.text = t_speed.ToString();
    }


    public void UpdateEquippedPotion(Sprite t_sprite)
    {
        if (t_sprite == null)
        {
            equippedPotion.sprite = emptySlotSprite;
        }
        else
        {
            equippedPotion.sprite = t_sprite;
        }
    }

    public void UpdateEquippedWeapon(Sprite t_sprite)
    {
        if (t_sprite == null)
        {
            equippedWeapon.sprite = emptySlotSprite;
        }
        else
        {
            equippedWeapon.sprite = t_sprite;
        }
    }
}
