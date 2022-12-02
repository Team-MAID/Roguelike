using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IConsumable
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

    [SerializeField] private ConsumableItemSO consumableItemData;

    [SerializeField]
    private GameObject player;
    string potionType;
    float m_multiplier;
    public HUD hud;

    // Start is called before the first frame update
    void Start()
    {
        consumableItemData.ConsumingItem += Consume;
        potionType = this.gameObject.tag;
        player = GameObject.Find("Player");
    }

    public void Consume(GameObject consumer)
    {
        hud.UpdateEquipedPotion(this.gameObject.GetComponent<SpriteRenderer>().sprite);
        m_multiplier = 2.0f;
        if (potionType == "SpeedUpPotion")
        {
            player.GetComponent<playerStats>().setSpeed(m_multiplier);
        }
        else if (potionType == "DefenseUpPotion")
        {
            player.GetComponent<playerStats>().setAttackDamage(m_multiplier);
        }
        else if (potionType == "AttackUpPotion")
        {
            player.GetComponent<playerStats>().setDefense(m_multiplier);
        }
        else if (potionType == "RefillHealthPotion")
        {
            player.GetComponent<HealthSystem>().IncreaseHealth(20);
            player.GetComponent<playerStats>().isPotionActive = false;
        }
        else if (potionType == "MysteryPotion")
        {
            useMysteryPotion();
        }
    }

        public void useStandardPotion()
    {
       
    }

    void useMysteryPotion()
    {
        m_multiplier = 0.5f;
        int temp_randomNumber = Random.Range(0, 8);
        m_mysteryPotionEffects = (mysteryPotionEffects)temp_randomNumber;
        hud.UpdateEquipedPotion(this.gameObject.GetComponent<SpriteRenderer>().sprite);
        // for testing 
        //m_mysteryPotionEffects = (mysteryPotionEffects)3;
        //Debug.Log(temp_randomNumber);
        if (m_mysteryPotionEffects == mysteryPotionEffects.loseHeart)
        {
            player.GetComponent<HealthSystem>().DecreaseHealth(20);
            player.GetComponent<playerStats>().isPotionActive = false;
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
            player.GetComponent<PlayerController>().coins += 10;
            player.GetComponent<playerStats>().isPotionActive = false;
            hud.UpdateCoinText(player.GetComponent<PlayerController>().coins);
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.immuneToDamage)
        {
            player.GetComponent<PlayerController>().setImmunity();
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.allBaseStatsUp)
        {
            player.GetComponent<playerStats>().increaseAllStats();
            player.GetComponent<playerStats>().isPotionActive = false;
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.maxHealthUp)
        {
            player.GetComponent<HealthSystem>().IncreaseHealth(20);
            player.GetComponent<playerStats>().isPotionActive = false;
        }
    }
}
