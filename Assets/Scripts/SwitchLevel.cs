using System;
using DungeonGeneration;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchLevel : MonoBehaviour
{
    private BSPDungeonLevelGenerator _levelGenerator;
    private bool _isPlayerOnStair = false;
    private TextMeshProUGUI _informationText;

    private void Start()
    {
        _levelGenerator = GameObject.FindWithTag("DungeonGenerator").GetComponent<BSPDungeonLevelGenerator>();

        _informationText = GameObject.Find("UI/InformationText").GetComponent<TextMeshProUGUI>();
        _informationText.SetText("Press U to use the stairs. WARNING: You cannot come back.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _isPlayerOnStair = true;
        _informationText.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _isPlayerOnStair = false;
        _informationText.enabled = false;
    }

    private void OnSwitchLevel()
    {
        if (_isPlayerOnStair)
        {
            _isPlayerOnStair = false;
            _levelGenerator.GenerateRandomLevel();
        }
    }
}