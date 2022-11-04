using System;
using System.Text;
using UnityEngine;

/// <summary>
/// Describe a Scriptable Object to create Weapon
/// </summary>
[CreateAssetMenu(fileName = "WeaponItemData", menuName = "Items/Weapon")]
public class WeaponItemData : ItemData
{
    [SerializeField, Header("Quantity of damage given by this weapon")]
    private float damage;
    
    public override string GetItemDescriptionText()
    {
        var builder = new StringBuilder();
        
        // TODO: implement this method

        return builder.ToString();
    }
}