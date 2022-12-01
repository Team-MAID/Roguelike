using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchLevel : MonoBehaviour
{
    private bool _isPlayerOnStair = false;
    private bool _isSwitchLevelPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }

    private void OnSwitchLevel(InputValue inputValue)
    {
        _isSwitchLevelPressed = inputValue.isPressed;
    }
}