using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    bool used;
    bool finished;

    float m_multiplier;
    float m_damageMultiplier;
    float m_defenseMultiplier;

    float m_maxHealth;
    float m_immuneToDamage;

    void setMultiplier(float t_multiplier)
    {
        m_multiplier = t_multiplier;
    }

    // speed potion
    // full health
    // extra Damage
    // Take no damage
    // Half Damage
    // Mystery potion

    // Neg effects
    // Lose a heart
    // Take Double damage
    // Deal half damage
    // speed halved




    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            m_multiplier = 2;
            player.GetComponent<playerStats>().setSpeed(m_multiplier);
        }
        else
        {
            used = false;
            m_multiplier = 1;
        }
    }
}
