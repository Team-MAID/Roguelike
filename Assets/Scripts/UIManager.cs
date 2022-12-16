using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    public GameObject GameOverMenu;
    public GameObject m_player;


    public void RestartGame()
    {
        SceneManager.LoadScene("FloorOne");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnEnable()
    {
            Collision.OnPlayerDeath += EnableGameOvermenu;
    }

    private void OnDisable()
    {
            Collision.OnPlayerDeath -= EnableGameOvermenu;
    }

    // event action has to have void return type and take no arguments

    public void EnableGameOvermenu()
    {
            GameOverMenu.SetActive(true);
    }


}
