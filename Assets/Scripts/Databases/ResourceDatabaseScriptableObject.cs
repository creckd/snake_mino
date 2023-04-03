using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ResourceDatabaseScriptableObject", order = 1)]
public class ResourceDatabaseScriptableObject : ScriptableObject
{
    public LevelTile _tilePrefab;
    public Snake _snakePrefab;
    public SnakeBodyPart _snakeBodyPartPrefab;
    public Apple _apple;
}
