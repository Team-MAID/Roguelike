using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IEquipable
{
    void Equip(GameObject user);
    void Unequip(GameObject user);
}
