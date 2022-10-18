using UnityEngine;
using System;
using System.Collections.Generic;	
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	
	private int columns; 
	private int rows;
	public GameObject exit;											
	public GameObject[] floorTiles;							
	public GameObject[] outerWallTiles;							
		
	private Transform boardHolder;
	private List <Vector3> gridPositions = new List <Vector3> ();
	private List<Vector3> outerWallPositions = new List<Vector3>();
	private List<Vector3> cornerPositions = new List<Vector3>();
	private List<int> possibleSize = new List<int>();

    private void Awake()
    {
		possibleSize.Add(4);
		possibleSize.Add(6);
		possibleSize.Add(8);

	}
    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList (int offsetX,int offsetY)
	{
		columns = possibleSize[Random.Range(0, possibleSize.Count)];
		rows = possibleSize[Random.Range(0, possibleSize.Count)];
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
		cornerPositions.Add(new Vector3(offsetX - 1, offsetY - 1, 0f));
		cornerPositions.Add(new Vector3(offsetX - 1, offsetY + rows, 0f));
		cornerPositions.Add(new Vector3(offsetX + columns, offsetY - 1, 0f));
		cornerPositions.Add(new Vector3(offsetX + columns, offsetY + rows, 0f));
	}
		
		
	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup (int boardNr,int boardOffsetX, int boardOffsetY)
	{
		int boardOriginX = 0;
		int boardOriginY = 0;

		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;

		if(boardNr != 0)
        {
			boardOriginX = boardOffsetX;
			boardOriginY = boardOffsetY;
		}
			
		for(int x = -1 + boardOriginX; x < columns + 1 + boardOriginX; x++)
		{
			for(int y = -1 + boardOriginY; y < rows + 1 + boardOriginY; y++)
			{
				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				if (x == -1 + boardOriginX || x == columns + boardOriginX || y == -1 + boardOriginY || y == rows + boardOriginY)
				{
					toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
				}


				for (int i = 0; i < cornerPositions.Count; i++)
				{
					if (x == -1 + boardOriginX || x == columns + boardOriginX || y == -1 + boardOriginY || y == rows + boardOriginY)
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
		int randRoomNr = Random.Range(3, 5);
		randRoomNr = 2;
		int offsetX = 0;
		int offsetY = 0;
		string exitSide = "None";

		Vector3 exitPos = new Vector3( 0, 0, 0 );

		for (int j = 0; j < randRoomNr; j++)
		{

			InitialiseList(offsetX,offsetY);

			BoardSetup(j,offsetX,offsetY);

			if (j==0)
            {
				exitPos = outerWallPositions[Random.Range(0, outerWallPositions.Count)];
				clearExit(exitPos);
			}

			if (exitPos.y == offsetY + rows)
            {
				exitSide = "North";
				offsetX = (int)exitPos.x - Mathf.FloorToInt(columns / 2);
				offsetY = (int)exitPos.y + 1;
			}
			else if (exitPos.y == offsetY - 1)
			{
				exitSide = "South";
				offsetX = (int)exitPos.x - Mathf.FloorToInt(columns / 2);
				offsetY = (int)exitPos.y - rows;
			}
			else if (exitPos.x== offsetX + columns)
			{
				exitSide = "East";
				offsetX = (int)exitPos.x + 1;
				offsetY = (int)exitPos.y - Mathf.FloorToInt(rows / 2);
			}
			else if (exitPos.x == offsetX - 1)
			{
				exitSide = "West";
				offsetX = (int)exitPos.x - columns ;
				offsetY = (int)exitPos.y - Mathf.FloorToInt(rows / 2);
			}
			Debug.Log(exitSide);

			clearExit(exitPos);
			if (j != 0)
			{
				exitPos = outerWallPositions[Random.Range(0, outerWallPositions.Count)];
			}
			for (int i = 0; i < cornerPositions.Count; i++)
			{
				while (exitPos.x == cornerPositions[i].x && exitPos.y == cornerPositions[i].y)
				{
					exitPos = outerWallPositions[Random.Range(0, outerWallPositions.Count)];
				}
			}

			Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], exitPos, Quaternion.identity);

			//Finds the wall sprite hidden behind door and sets it to inactive, allows player to pass through door
			for (int i = 0; i < boardHolder.childCount; i++)
			{
				if (boardHolder.GetChild(i).transform.position == exitPos)
				{
					boardHolder.GetChild(i).gameObject.SetActive(false);
				}
			}
		}
	}

	private void clearExit(Vector3 exit)
    {
		for (int i = 0; i < boardHolder.childCount; i++)
		{
			if (boardHolder.GetChild(i).transform.position == exit)
			{
				Destroy(boardHolder.GetChild(i).gameObject);
			}
		}
	}
}
