using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        /* TODO: Please, fix the line below, it causes compile error when building the game (I had to comment it, otherwise the game won't build
         The EditorApplication is part of the UnityEditor namespace which is only accessible 
         in the context of Unity, you cannot use this class in the built game */
        //EditorApplication.isPlaying = false;
    }
}