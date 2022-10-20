using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject exitTile;
    [SerializeField] private GameObject[] floorTiles;
    [SerializeField] private GameObject[] outerWallTiles;

    private Room _room;

    private GameObject _roomHolder;

    private void Start()
    {
        _room = new Room();
        
        CreateRoom();
    }

    /// <summary>
    /// Instantiate the tiles in the Scene to create the room
    /// </summary>
    private void CreateRoom()
    {
        _roomHolder = new GameObject($"Room ({_room.Columns}x{_room.Rows})");

        foreach (Vector2Int position in _room.OuterWallPositions)
        {
            InstantiateOuterWall(position.x, position.y);
        }

        foreach (Vector2Int position in _room.GridPositions)
        {
            InstantiateFloor(position.x, position.y);
        }

        foreach (Vector2Int position in _room.CornerPositions)
        {
            InstantiateOuterWall(position.x, position.y);
        }
    }

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
}