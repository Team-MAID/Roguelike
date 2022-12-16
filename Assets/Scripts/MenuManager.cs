using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject instruc;
    public GameObject settings;
    public void StartGame()
    {
        SceneManager.LoadScene("FloorOne");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OpenInstructions()
    {
        menu.SetActive(false);
        instruc.SetActive(true);
    }

    public void CloseInstructions()
    {
        menu.SetActive(true);
        instruc.SetActive(false);
    }

    public void OpenSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    } 
    public void CloseSettings()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }
}