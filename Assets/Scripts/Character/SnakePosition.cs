using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESnakeSpawnOrientationType
{
    Vertical,
    Horizontal
}

[System.Serializable]
public class SnakeSpawnPoint
{
    public Vector2Int _headPosition;
    public ESnakeSpawnOrientationType _spawnOrientationType;
}
