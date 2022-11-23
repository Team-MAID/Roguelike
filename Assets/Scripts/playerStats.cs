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

    [SerializeField]
    protected float attack;
    [SerializeField]
    protected float defense;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float potionCoolDown;

    // Increasing stats based on potion used
    public void setAttackDamage(float t_multiplier)
    {
        attack = baseAttack * t_multiplier + weaponDamage;
        StartCoroutine(potionDuration());
    }
    public void setSpeed(float t_multiplier)
    {
        speed = baseSpeed * t_multiplier;
        StartCoroutine(potionDuration());
    }
    public void setDefense(float t_multiplier)
    {
        defense = baseDefense * t_multiplier + armourDefense;
        StartCoroutine(potionDuration());
    }
    public void increaseAllStats()
    {
        baseAttack += 2;
        baseSpeed += 0.5f;
        baseDefense += 2;
        setAttackDamage();
        setDefense();
        GetComponent<HealthSystem>().IncreaseMaxHealth(20);
        setSpeed();
    }

    public void setImmunity()
    {
        isImmuneTodamage = true;
        StartCoroutine(potionDuration());
    }

    IEnumerator potionDuration()
    {
        yield return new WaitForSeconds(potionCoolDown);
        setSpeed();
        setDefense();
        setAttackDamage();
        isImmuneTodamage = false;
        isPotionActive = false;
    }

    // resetting to base stats after potion wears out
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
}
