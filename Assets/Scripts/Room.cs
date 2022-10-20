using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room
{
    private const int MinRoomSize = 4;
    private const int MaxRoomSize = 10;

    public int Rows { get; }
    public int Columns { get; }

    public List<Vector2Int> GridPositions { get; } = new();
    public List<Vector2Int> CornerPositions { get; } = new();
    public List<Vector2Int> OuterWallPositions { get; } = new();


    private GameObject _roomHolder;

    public Room()
    {
        Rows = Random.Range(MinRoomSize, MaxRoomSize);
        Columns = Random.Range(MinRoomSize, MaxRoomSize);

        InitialisePositions();

        Debug.Log($"Columns: {Columns} | Rows: {Rows}.");
    }

    /// <summary>
    /// Initialise lists with positions
    /// </summary>
    private void InitialisePositions()
    {
        // Create corner positions for the room
        CornerPositions.Add(new Vector2Int(0, 0));
        CornerPositions.Add(new Vector2Int(0, Rows));
        CornerPositions.Add(new Vector2Int(Columns, 0));
        CornerPositions.Add(new Vector2Int(Columns, Rows));

        for (var x = 0; x <= Columns; x++)
        {
            for (var y = 0; y <= Rows; y++)
            {
                if ((x > 0 && x < Columns && (y == 0 || y == Rows)) || 
                    (y > 0 && y < Rows && (x == 0 || x == Columns)))
                {
                    OuterWallPositions.Add(new Vector2Int(x, y));
                }
                
                if (x != 0 && x != Columns && y != 0 && y != Rows)
                {
                    GridPositions.Add(new Vector2Int(x, y));
                }
            }
        }
    }
}