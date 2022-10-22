using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public enum CardinalPoint
{
    None,
    North,
    South,
    East,
    West,
}

public class BoardManager : MonoBehaviour
{
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;

    private const int MinRoomSize = 4;
    private const int MaxRoomSize = 8;

    private int _columns;
    private int _rows;

    private Transform _boardHolder;
    private List<Vector2> _gridPositions = new();
    private List<Vector2> _outerWallPositions = new();
    private List<Vector2> _cornerPositions = new();
    private List<int> _possibleSize = new();

    private void Awake()
    {
        _possibleSize.Add(4);
        _possibleSize.Add(6);
        _possibleSize.Add(8);
    }

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList(int offsetX, int offsetY)
    {
        /*columns = possibleSize[Random.Range(0, possibleSize.Count)];
        rows = possibleSize[Random.Range(0, possibleSize.Count)];*/

        _columns = Random.Range(MinRoomSize, MaxRoomSize);
        _rows = Random.Range(MinRoomSize, MaxRoomSize);

        //Clear our list gridPositions.
        _gridPositions.Clear();
        _cornerPositions.Clear();
        _outerWallPositions.Clear();

        for (int x = 1; x < _columns - 1; x++)
        {
            for (int y = 1; y < _rows - 1; y++)
            {
                // Fill position for middle of the room
                // At each index add a new Vector2 to our list with the x and y coordinates of that position.
                _gridPositions.Add(new Vector2(x, y));
            }
        }

        // Generate the corner positions for the room
        _cornerPositions.Add(new Vector2(offsetX - 1, offsetY - 1));
        _cornerPositions.Add(new Vector2(offsetX - 1, offsetY + _rows));
        _cornerPositions.Add(new Vector2(offsetX + _columns, offsetY - 1));
        _cornerPositions.Add(new Vector2(offsetX + _columns, offsetY + _rows));
    }

    // Sets up the outer walls and floor (background) of the game board.
    void BoardSetup(int boardNr, int boardOffsetX, int boardOffsetY)
    {
        int boardOriginX = boardOffsetX;
        int boardOriginY = boardOffsetY;

        // Instantiate Board and set boardHolder to its transform.
        _boardHolder = new GameObject("Board").transform;

        /*if (boardNr != 0)
        {
            boardOriginX = boardOffsetX;
            boardOriginY = boardOffsetY;
        }*/

        for (int x = -1 + boardOriginX; x < _columns + 1 + boardOriginX; x++)
        {
            for (int y = -1 + boardOriginY; y < _rows + 1 + boardOriginY; y++)
            {
                // Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                // Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                if (x == -1 + boardOriginX || x == _columns + boardOriginX || y == -1 + boardOriginY ||
                    y == _rows + boardOriginY)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }


                for (int i = 0; i < _cornerPositions.Count; i++)
                {
                    if (x == -1 + boardOriginX || x == _columns + boardOriginX || y == -1 + boardOriginY ||
                        y == _rows + boardOriginY)
                    {
                        if (x != _cornerPositions[i].x && y != _cornerPositions[i].y)
                        {
                            _outerWallPositions.Add(new Vector3(x, y, 0f));
                        }
                    }
                }

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(_boardHolder);
            }
        }
    }

    public void SetupScene()
    {
        int maxRoomNumber = Random.Range(2, 3); // Return only 2

        // Offset between rooms (so room does not overlap)
        int offsetX = 0;
        int offsetY = 0;
        CardinalPoint exitSide = CardinalPoint.North;

        Vector2 exitPos = new Vector2(0, 0);

        for (int j = 0; j < maxRoomNumber; j++)
        {
            InitialiseList(offsetX, offsetY);

            BoardSetup(j, offsetX, offsetY);

            if (j == 0)
            {
                exitPos = _outerWallPositions[Random.Range(0, _outerWallPositions.Count)];
                clearExit(exitPos);
            }

            if (exitPos.y == offsetY + _rows)
            {
                exitSide = CardinalPoint.North;
                offsetX = (int) exitPos.x - Mathf.FloorToInt(_columns / 2);
                offsetY = (int) exitPos.y + 1;
            }
            else if (exitPos.y == offsetY - 1)
            {
                exitSide = CardinalPoint.South;
                offsetX = (int) exitPos.x - Mathf.FloorToInt(_columns / 2);
                offsetY = (int) exitPos.y - _rows;
            }
            else if (exitPos.x == offsetX + _columns)
            {
                exitSide = CardinalPoint.East;
                offsetX = (int) exitPos.x + 1;
                offsetY = (int) exitPos.y - Mathf.FloorToInt(_rows / 2);
            }
            else if (exitPos.x == offsetX - 1)
            {
                exitSide = CardinalPoint.West;
                offsetX = (int) exitPos.x - _columns;
                offsetY = (int) exitPos.y - Mathf.FloorToInt(_rows / 2);
            }

            Debug.Log(exitSide.ToString());

            clearExit(exitPos);
            if (j != 0)
            {
                exitPos = _outerWallPositions[Random.Range(0, _outerWallPositions.Count)];
            }

            for (int i = 0; i < _cornerPositions.Count; i++)
            {
                while (exitPos.x == _cornerPositions[i].x && exitPos.y == _cornerPositions[i].y)
                {
                    exitPos = _outerWallPositions[Random.Range(0, _outerWallPositions.Count)];
                }
            }

            Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], exitPos, Quaternion.identity);

            //Finds the wall sprite hidden behind door and sets it to inactive, allows player to pass through door
            for (int i = 0; i < _boardHolder.childCount; i++)
            {
                if ((Vector2) _boardHolder.GetChild(i).transform.position == exitPos)
                {
                    _boardHolder.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    private void clearExit(Vector3 exit)
    {
        for (int i = 0; i < _boardHolder.childCount; i++)
        {
            if (_boardHolder.GetChild(i).transform.position == exit)
            {
                Destroy(_boardHolder.GetChild(i).gameObject);
            }
        }
    }
}