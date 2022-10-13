﻿using UnityEngine;
using System;
using System.Collections.Generic;	
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	[Serializable]
	public class Count
	{
		public int minimum;		
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
		
	public int columns = 8; 
	public int rows = 8;
	public GameObject exit;											
	public GameObject[] floorTiles;							
	public GameObject[] outerWallTiles;							
		
	private Transform boardHolder;
	private List <Vector3> gridPositions = new List <Vector3> ();
	private List<Vector3> outerWallPositions = new List<Vector3>();
	private List<Vector3> cornerPositions = new List<Vector3>();
		
		
	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();
		cornerPositions.Clear();
		outerWallPositions.Clear();

		for (int x = 1; x < columns-1; x++)
		{
			for(int y = 1; y < rows-1; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
		cornerPositions.Add(new Vector3(-1, -1, 0f));
		cornerPositions.Add(new Vector3(-1, rows, 0f));
		cornerPositions.Add(new Vector3(columns, -1, 0f));
		cornerPositions.Add(new Vector3(columns, rows, 0f));
	}
		
		
	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
        Debug.Log(cornerPositions[0]);
        Debug.Log(cornerPositions[1]);
        Debug.Log(cornerPositions[2]);
        Debug.Log(cornerPositions[3]);

		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;
			
		for(int x = -1; x < columns + 1; x++)
		{
			for(int y = -1; y < rows + 1; y++)
			{
				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				if (x == -1 || x == columns || y == -1 || y == rows)
				{
					toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
				}


				for (int i = 0; i < cornerPositions.Count; i++)
				{
					if (x == -1 || x == columns || y == -1 || y == rows)
					{
						if (x != cornerPositions[i].x && y != cornerPositions[i].y)
						{
							outerWallPositions.Add(new Vector3(x, y, 0f));
						}
					}
				}
				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					
				instance.transform.SetParent (boardHolder);
			}
		}
	}
	public void SetupScene ()
	{
		InitialiseList();

		BoardSetup();

		Vector3 exitPos = outerWallPositions[Random.Range(0, outerWallPositions.Count)];
		for (int i = 0; i < cornerPositions.Count; i++)
		{
			if (exitPos.x == cornerPositions[i].x && exitPos.y == cornerPositions[i].y)
			{
				exitPos = outerWallPositions[Random.Range(0, outerWallPositions.Count)];
			}
		}

		Instantiate (exit, exitPos, Quaternion.identity);

		//Finds the wall sprite hidden behind door and sets it to inactive, allows player to pass through door
		for (int i = 0; i < boardHolder.childCount;i++)
        {
			if(boardHolder.GetChild(i).transform.position == exitPos)
            {
				boardHolder.GetChild(i).gameObject.SetActive(false);
			}
        }
	}
}
