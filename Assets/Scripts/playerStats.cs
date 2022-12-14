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

    // Increasing stats based on potion used
    public void setAttackDamagePotion(float t_multiplier)
    {
        storeAttack = attack;
        attack = (baseAttack + weaponDamage) * t_multiplier;
        StartCoroutine(potionDuration());
    }
    public void setSpeed(float t_multiplier)
    {
        speed = baseSpeed * t_multiplier;
        StartCoroutine(potionDuration());
    }
    public void setDefense(float t_multiplier)
    {
        defense = 0.5f;
        //defense = baseDefense * t_multiplier + armourDefense;
        StartCoroutine(potionDuration());
    }
    public float getDefense()
    {
        return defense;
    }
    public void increaseAllStats()
    {
        baseAttack += 2;
        baseSpeed += 0.5f;
        baseDefense += 2;
        setAttackDamage(0);
        defense = 0.0f;
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
        defense = 0.0f;
        if (storeAttack!=0)
        {
            attack = storeAttack;
        }
        isImmuneTodamage = false;
        isPotionActive = false;
    }

    // resetting to base stats after potion wears out
    public void setAttackDamage(int t_weaponDamage)
    {
        weaponDamage = t_weaponDamage;
        attack = baseAttack + weaponDamage;
    }
    public int getAttackDamage()
    {
        return (int)attack;
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
