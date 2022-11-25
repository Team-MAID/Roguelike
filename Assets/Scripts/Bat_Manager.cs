 using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bat_Manager : MonoBehaviour
{
    public GameObject _batPrfb;
    public int _batCount;
    private Bat_Controller[] _bats;
    
    void Start()
    {
        //spawnBats();    
    }

    void Update()
    {

    }

    public void ActivateTheBats()
    {
       Debug.Log("ACTIVATE.....THE BATS!");
       _bats = FindObjectsOfType<Bat_Controller>();

        foreach(Bat_Controller bat in _bats)
        {
            bat._enabled = true;
        }
    }

    public void spawnBats()
    {
        for (int i = 0; i < _batCount; i++)
        {
            Vector3 _offset = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            Instantiate(_batPrfb, transform.position + _offset, Quaternion.identity, transform);
        }
    }
}
