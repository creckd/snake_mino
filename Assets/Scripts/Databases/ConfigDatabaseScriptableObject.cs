using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConfigDatabaseScriptableObject", order = 1)]
public class ConfigDatabaseScriptableObject : ScriptableObject
{
    [Header("Main configurations")]
    public float _tickFrequency = 1;
    public int _levelHeight = 5;
    public int _levelWidth = 5;
    public int _startingCharacterSize = 3;

    [Header("Presentation")]
    public float _gridSizeInWorldSpace = 1f;
    public Color[] snakeColorPalette;

    [Header("Gameplay")]
    public SnakeSpawnPoint[] _snakeSpawnPoints;
}
