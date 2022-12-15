using UnityEngine;
using Items.ScriptableObjects;
using UnityEngine.Serialization;

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


    [SerializeField]private GameObject player;
    string potionType;
    float m_multiplier;

    // Start is called before the first frame update
    void Start()
    {
        potionType = this.gameObject.tag;
        player = GameObject.Find("Player");
    }

    public void useStandardPotion()
    {
        m_multiplier = 2.0f;
        if (potionType == "SpeedUpPotion")
        {
            player.GetComponent<playerStats>().setSpeed(m_multiplier);
        }
        else if (potionType == "AttackUpPotion")
        {
            player.GetComponent<playerStats>().setAttackDamagePotion(m_multiplier);
        }
        else if (potionType == "DefenseUpPotion")
        {
            player.GetComponent<playerStats>().setDefense(m_multiplier);
        }
        else if (potionType == "RefillHealthPotion")
        {
            player.GetComponent<HealthSystem>().IncreaseHealth(20);
            player.GetComponent<playerStats>().isPotionActive = false;
        }
    }

    void useMysteryPotion()
    {
        m_multiplier = 0.5f;
        int temp_randomNumber = Random.Range(0, 8);
        m_mysteryPotionEffects = (mysteryPotionEffects)temp_randomNumber;

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
            player.GetComponent<playerStats>().setAttackDamagePotion(m_multiplier);
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.speedDown)
        {
            player.GetComponent<playerStats>().setSpeed(m_multiplier);
        }
        else if (m_mysteryPotionEffects == mysteryPotionEffects.coins)
        {
            player.GetComponent<PlayerController>().coins += 10;
            player.GetComponent<playerStats>().isPotionActive = false;
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

    private void OnTriggerEnter2D(Collider2D _otherColldier)
    {
        if (_otherColldier.gameObject.CompareTag("Player"))
        {
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
                Destroy(this.gameObject);
            }
        }
    }

    public void Consume(GameObject consumer)
    {
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
            Destroy(this.gameObject);
        }
    }
}
