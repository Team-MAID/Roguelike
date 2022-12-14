using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopDudeScript : MonoBehaviour
{
    private GameObject m_player;
    public GameObject m_shop;
    private GameObject m_shopInstance;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }
    /// <summary>
    /// Instantiates the shop window when the player is within range of the shop dude and presses E
    /// <summary>
    void Update()
    {
        if (m_player.gameObject != null)
        {
            float distance = Vector3.Distance(m_player.transform.position, gameObject.transform.position);
            if (distance <= 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (m_shopInstance)
                    {
                        Destroy(m_shopInstance);
                    }
                    else
                        m_shopInstance = Instantiate(m_shop);
                }
            }
        }
    }
}
