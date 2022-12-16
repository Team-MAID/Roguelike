 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Manager : MonoBehaviour
{
    private Bat_Controller[] _bats;

    void Start()
    {
    }
    public int ActivateTheBats()
    {
        Debug.Log("ACTIVATE.....THE BATS!");
        _bats = FindObjectsOfType<Bat_Controller>();
        int counter = 0;
        foreach (Bat_Controller bat in _bats)
        {
            // bat.batAttacked = true;
            counter++;
        }
        return counter;
    }
}
