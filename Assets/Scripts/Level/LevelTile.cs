using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    private int _x = -1;
    private int _y = -1;

    private List<LevelObject> _levelObjectsOnTheTile = new List<LevelObject>();

    public void SetPosition(Vector2Int position)
    {
        _x = position.x;
        _y = position.y;
    }

    public void UseTile(LevelObject usingObject)
    {
        _levelObjectsOnTheTile.Add(usingObject);
    }

    public void FreeTile(LevelObject freeingObject)
    {
        _levelObjectsOnTheTile.Remove(freeingObject);
    }
}
