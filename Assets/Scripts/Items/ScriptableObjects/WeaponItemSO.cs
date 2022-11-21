using System;
using System.Text;
using InventorySystem.ScriptableObjects;
using UnityEngine;

/// <summary>
/// Describe a Scriptable Object to create Weapon
/// </summary>
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Weapon Item")]
public class WeaponItemSO : ItemSO
{
    [SerializeField, Header("Quantity of damage given by this weapon")]
    private float damage;
}