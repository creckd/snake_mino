using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    private int _x = -1;
    private int _y = -1;

    private LevelObject _levelObjectOnThisTile = null;

    public void SetPosition(Vector2Int position)
    {
        _x = position.x;
        _y = position.y;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(_x, _y);
    }

    public void UseTile(LevelObject usingObject)
    {
        // Collision
        if (_levelObjectOnThisTile != null)
        {
            GameController.Instance.OnCollision(usingObject, _levelObjectOnThisTile);
            return;
        }

        _levelObjectOnThisTile = usingObject;
    }

    public void FreeTile(LevelObject freeingObject)
    {
        _levelObjectOnThisTile = null;
    }

    public bool IsTotallyFree()
    {
        return _levelObjectOnThisTile == null;
    }
}
