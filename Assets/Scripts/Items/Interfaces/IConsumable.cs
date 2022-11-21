using System;
using UnityEngine;

/// <summary>
/// Items that can be consumed (eg: Health Potion) must implement this interface
/// </summary>
public interface IConsumable
{
    void Consume(GameObject consumer);
}