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
    void Update()
    {
        float distance = Vector3.Distance(m_player.transform.position, gameObject.transform.position);
        if(distance <= 1)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {    
                m_shopInstance = Instantiate(m_shop);
            }
        }
        else
        {
            if (m_shop != null)
            {
                Destroy(m_shopInstance);   
            }
        }
    }
}
