using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public void buyBeef()
    {
        Debug.Log("Beef");
    }
    public void buyRedBook()
    {
        Debug.Log("RedBook");
    }
    public void buyBlueBook()
    {
        Debug.Log("BlueBook");
    }
    public void buyCandy()
    {
        Debug.Log("Candy");
    }
    public void buyLog()
    {
        Debug.Log("Log");
    }
    public void exit()
    {
        Destroy(gameObject);
    }
}
