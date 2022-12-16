using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    public GameObject GameOverMenu;
    public GameObject m_player;
    private GameObject m_enemy;

    private void Awake()
    {
        StartCoroutine(checkForEnemiesAlive());
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("FloorOne");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator checkForEnemiesAlive()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (SceneManager.GetActiveScene().name == "FloorFour")
            {
                print(GameObject.FindGameObjectsWithTag("Enemy").Length);

                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    SceneManager.LoadScene("Cleared");
                }
            }
        }
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
