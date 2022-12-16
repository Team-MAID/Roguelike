using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

/// <summary>
/// Class <c>Potion</c> Manages Potions and their effects
/// </summary>
public class Potion : MonoBehaviour
{
    /// <summary>
    /// Enum <c>mysteryPotionEffects</c> sets the possible effects of a mystery potion to an enum
    /// </summary>
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


    [SerializeField] private GameObject player;
    string potionType;
    float m_multiplier;
    
    private Item _item;
    private HUD _hud;
    private ConsumableItemSO _consumableItemData;

    
    void Start()
    {
        _hud = FindObjectOfType<HUD>();
        
        potionType = this.gameObject.tag;
        player = GameObject.Find("Player");
        
        _item = GetComponent<Item>();
        _consumableItemData = _item.ItemData as ConsumableItemSO;
        
        _consumableItemData.ConsumingItem += Consume;

    }

    /// <summary>
    /// Method <c>useStandardPotion</c> Manages standard potion effects when a potion is consumed
    /// </summary>
    public void useStandardPotion()
    {
        m_multiplier = 2.0f;
        if (potionType == "SpeedUpPotion")
        {
            player.GetComponent<playerStats>().setSpeedPotion(m_multiplier);
        }
        else if (potionType == "AttackUpPotion")
        {
            player.GetComponent<playerStats>().setAttackDamagePotion(m_multiplier);
        }
        else if (potionType == "DefenseUpPotion")
        {
            player.GetComponent<playerStats>().setDefensePotion(m_multiplier);
        }
        else if (potionType == "RefillHealthPotion")
        {
            player.GetComponent<HealthSystem>().IncreaseHealth(20);
            player.GetComponent<playerStats>().isPotionActive = false;
        }
    }

    /// <summary>
    /// Method <c>useMysteryPotion</c> Manages mystery potion effects when a mystery potion is consumed.
    /// The effect is random.
    /// </summary>
    void useMysteryPotion()
    {
        m_multiplier = 0.5f;
        int temp_randomNumber = Random.Range(0, 8);
        m_mysteryPotionEffects = (mysteryPotionEffects)temp_randomNumber;

        // for testing 
        //m_mysteryPotionEffects = (mysteryPotionEffects)6;
        //Debug.Log(temp_randomNumber);
        if (m_mysteryPotionEffects == mysteryPotionEffects.loseHeart)
        {
            player.GetComponent<HealthSystem>().DecreaseHealth(20);
            player.GetComponent<playerStats>().isPotionActive = false;
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.defenseDown)
        {
            player.GetComponent<playerStats>().setDefensePotion(m_multiplier);
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.attackDown)
        {
            player.GetComponent<playerStats>().setAttackDamagePotion(m_multiplier);
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.speedDown)
        {
            player.GetComponent<playerStats>().setSpeedPotion(m_multiplier);
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.coins)
        {
            player.GetComponent<PlayerController>().coins += 10;
            player.GetComponent<playerStats>().isPotionActive = false;
            _hud.UpdateCoinText(player.GetComponent<PlayerController>().coins);
            _hud.UpdateEquippedPotion(this.gameObject.GetComponent<SpriteRenderer>().sprite);
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

    /// <summary>
    /// Method <c>Consume</c> Determines wheter a standard or mystery potion was used and calls
    /// the appropriate method.
    /// </summary>
    /// <param name="consumer"></param>
    public void Consume(GameObject consumer)
    {
        Debug.Log("Consuming potion");
        if (player.GetComponent<playerStats>().isPotionActive == false)//isActive on Player
        {
            player.GetComponent<playerStats>().isPotionActive = true;
            if (potionType != "MysteryPotion")
            {
                useStandardPotion();
            }
            else
            {
                useMysteryPotion();
            }
        }
    }
}
