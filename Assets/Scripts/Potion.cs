using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    enum mysteryPotionEffects
    {
        //Negative Effects
        loseHeart = 0,
        defenseDown = 1,
        attackDown = 2,
        speedDown = 3,
        //Positive Effects
        coins = 4,
        immuneToDamage = 5,
        allBaseStatsUp = 6,
        maxHealthUp = 7
    };

    mysteryPotionEffects m_mysteryPotionEffects;

    [SerializeField]
    private GameObject player;
    bool isPotionActive;
    string potionType;
    float m_multiplier;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void useStandardPotion(string t_potionType)
    {
        if (!isPotionActive)
        {
            // delegate int func;
            isPotionActive = true;
            m_multiplier = 2.0f;
            if (t_potionType == "potionSpeedUp")
            {
                player.GetComponent<playerStats>().setSpeed(m_multiplier);
                // func = player.GetComponent<playerStats>().setSpeed(m_multiplier);
                //resetPotionEffects(m_multiplier);
            }
            else if (t_potionType == "potionDefenseUp")
            {
                player.GetComponent<playerStats>().setAttackDamage(m_multiplier);
            }
            else if (t_potionType == "potionAttackUp")
            {
                player.GetComponent<playerStats>().setDefense(m_multiplier);
            }
            else if (t_potionType == "potionmaxHealth")
            {
               // player.GetComponent<playerStats>().setHealth();
            }
        }
    }

    void resetPotionEffects(float t_currentMultiplier)
    {
        player.GetComponent<playerStats>().setSpeed();
        player.GetComponent<playerStats>().setDefense();
        player.GetComponent<playerStats>().setDefense();
    }

    void useMysteryPotion(string t_potionType)
    {
        if (!isPotionActive)
        {
            isPotionActive = true;
            m_multiplier = 0.5f;
            int temp_randomNumber = Random.Range(0, 8);
            m_mysteryPotionEffects = (mysteryPotionEffects)temp_randomNumber;
            if (m_mysteryPotionEffects == mysteryPotionEffects.loseHeart)
            {

            }
            else if (m_mysteryPotionEffects == mysteryPotionEffects.defenseDown)
            {
                player.GetComponent<playerStats>().setDefense(m_multiplier);
            }
            else if (m_mysteryPotionEffects == mysteryPotionEffects.attackDown)
            {
                player.GetComponent<playerStats>().setAttackDamage(m_multiplier);
            }
            else if (m_mysteryPotionEffects == mysteryPotionEffects.speedDown)
            {
                player.GetComponent<playerStats>().setSpeed(m_multiplier);
            }
            else if (m_mysteryPotionEffects == mysteryPotionEffects.coins)
            {

            }
            else if (m_mysteryPotionEffects == mysteryPotionEffects.immuneToDamage)
            {

            }
            else if (m_mysteryPotionEffects == mysteryPotionEffects.allBaseStatsUp)
            {

            }
            else if (m_mysteryPotionEffects == mysteryPotionEffects.maxHealthUp)
            {
                //player.GetComponent<playerStats>().setHealth();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
