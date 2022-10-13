using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
	
public class GameManager : MonoBehaviour
{
	private BoardManager boardScript;				
		
		
	//Awake is always called before any Start functions
	void Awake()
	{
		boardScript = GetComponent<BoardManager>();
			
		InitGame();
	}
		
	//Initializes the game for each level.
	void InitGame()
	{
		boardScript.SetupScene();			
	}
		
}

