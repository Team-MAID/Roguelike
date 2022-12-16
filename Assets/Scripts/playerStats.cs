using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public bool isPotionActive;
    public bool isImmuneTodamage;

    protected float baseAttack;
    protected float baseDefense;
    protected int baseHealth;
    protected float baseSpeed;

    float weaponDamage;
    float armourDefense;
    private float storeAttack;
    [SerializeField]
    protected float attack;
    [SerializeField]
    protected float defense = 0.0f;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float potionCoolDown;
    public HUD hud;


    // Increasing stats based on potion used
    public void setAttackDamagePotion(float t_multiplier)
    {
        storeAttack = attack;
        attack = (baseAttack + weaponDamage) * t_multiplier;
        setHudStats();
        StartCoroutine(potionDuration());
    }
    public void setSpeedPotion(float t_multiplier)
    {
        speed = baseSpeed * t_multiplier;
        setHudStats();
        StartCoroutine(potionDuration());
    }
    public void setDefensePotion(float t_multiplier)
    {
        if (baseDefense <= 0.5f)
        {
            defense = baseDefense + 0.5f;
        }
        else
        {
            defense = 1.0f;
        }
        setHudStats();
        StartCoroutine(potionDuration());
    }

    public void increaseAllStats()
    {
        if (baseAttack <= 9.0f) { baseAttack += 1; }
        if (baseSpeed <= 5.5f) { baseSpeed += 0.5f; }
        if (baseDefense <= .4f) { baseDefense += 0.1f; }
        GetComponent<HealthSystem>().IncreaseMaxHealth(20);
        setBaseSpeed();
        setBaseDefense();
        setBaseAttack();
        setHudStats();
    }

    public void setImmunity()
    {
        isImmuneTodamage = true;
        StartCoroutine(potionDuration());
    }

    IEnumerator potionDuration()
    {
        yield return new WaitForSeconds(potionCoolDown);
        setBaseSpeed();
        setBaseDefense();
        setBaseAttack();
        if (storeAttack!=0)
        {
            attack = storeAttack;
        }
        isImmuneTodamage = false;
        isPotionActive = false;
        setHudStats();
    }

    // resetting to base stats after potion wears out
    public void setAttackDamageWithWeapon(int t_weaponDamage)
    {
        weaponDamage = t_weaponDamage;
        attack = baseAttack + weaponDamage;
        setHudStats();
    }
    public int getAttackDamage()
    {
        return (int)attack;
    }

    public void setBaseAttack()
    {
        attack = baseAttack + weaponDamage;
    }

    public int getBaseAttack()
    {
        return (int)attack;
    }

    public void setBaseDefense()
    {
        defense = baseDefense;
    }

    public float getDefense()
    {
        return defense;
    }

    public void setBaseSpeed()
    {
        speed = baseSpeed;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setHudStats()
    {
        hud.UpdateDefenseText((int)(getDefense() * 100));
        hud.UpdateAttackText(getAttackDamage());
        hud.UpdateSpeedText(getSpeed());
    }
}
