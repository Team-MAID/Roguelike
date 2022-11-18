using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    public GameObject _batPrfb;
    public int _batCount;
    private BatController[] _bats;
    void Start()
    {
        for (int i = 0; i < _batCount; i++)
        {
            //Debug.Log("Loop " + i);
            Vector3 _offset = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            Instantiate(_batPrfb, transform.position + _offset, Quaternion.identity, transform);
        }
    }

    void Update()
    {

    }

    public void ActivateTheBats()
    {
       //Debug.Log("ACTIVATE.....THE BATS!");
       _bats = FindObjectsOfType<BatController>();

        foreach(BatController bat in _bats)
        {
            bat._enabled = true;
        }
        
    }
}
