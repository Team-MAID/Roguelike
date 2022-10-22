/*using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CardinalPoint
{
    North,
    South,
    East,
    West,
}

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject exitTile;
    [SerializeField] private GameObject[] floorTiles;
    [SerializeField] private GameObject[] outerWallTiles;

    private GameObject _roomHolder;

    private List<Room> _rooms = new();

    private void Start()
    {
        SetupScene();
    }


    void SetupScene()
    {
        //var maxRoomNumber = Random.Range(2, 2);
        var maxRoomNumber = 2;

        var offsetX = 0;
        var offsetY = 0;

        for (int roomNumber = 1; roomNumber <= maxRoomNumber; roomNumber++)
        {
            var room = CreateRoom(roomNumber);
            _rooms.Add(room);

            // CardinalPoint randomDir = (CardinalPoint) Random.Range(0, Enum.GetNames(typeof(CardinalPoint)).Length);

            
            offsetX += room.Columns;
            offsetY += Random.Range(-room.Rows, room.Rows);
            /*if (randomDir == CardinalPoint.East)
            {
                offsetX += room.Columns;
                offsetY += Random.Range(-room.Rows, room.Rows);
            }
            else if (randomDir == CardinalPoint.West)
            {
                offsetX -= room.Columns;
                offsetY -= Random.Range(-room.Rows, room.Rows);
            }#1#
        }
    }

    /// <summary>
    /// Instantiate the tiles in the Scene to create the room
    /// </summary>
    /// <remarks>May be refactored as a Factory?</remarks>
    /*private Room CreateRoom(int roomNumber)
    {
        Room room;
        if (_rooms.Count == 0)
            room = new Room();
        else
        {
            
            
            room = new Room(new Vector2Int(10, 15));
        }

        /*if (roomNumber > 1)
        {
            offsetX += room.Columns / 2;
            offsetY += room.Rows / 2;
        }#2#

        _roomHolder = new GameObject($"Room {roomNumber} | ({room.Columns}x{room.Rows})");

        foreach (Vector2Int position in room.WorldOuterWallPositions)
        {
            InstantiateOuterWall(position.x, position.y);
        }

        foreach (Vector2Int position in room.WorldGridPositions)
        {
            InstantiateFloor(position.x, position.y);
        }

        foreach (Vector2Int position in room.WorldCornerPositions)
        {
            InstantiateOuterWall(position.x, position.y);
        }

        var floor = Instantiate(exitTile, new Vector2(room.WorldExitPosition.x, room.WorldExitPosition.y),
            Quaternion.identity);
        floor.transform.SetParent(_roomHolder.transform);

        return room;
    }#1#

    private void InstantiateFloor(int x, int y)
    {
        GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

        var floor = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
        floor.transform.SetParent(_roomHolder.transform);
    }

    private void InstantiateOuterWall(int x, int y)
    {
        var toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

        var outerWall = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
        outerWall.transform.SetParent(_roomHolder.transform);
    }
}*/