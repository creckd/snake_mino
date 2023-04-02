using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    protected Vector2Int _position = Vector2Int.zero;
    protected LevelTile _currentlyOnTile = null;

    public Vector2Int Position {
        get {
            return _position;
        }
        set {
            if (_position == value)
                return;

            if (_currentlyOnTile)
            {
                _currentlyOnTile.FreeTile(this);
                _currentlyOnTile = null;
            }

            _position = value;
            _currentlyOnTile = LevelController.Instance.GetTileAtPos(_position.x, _position.y);
            _currentlyOnTile.UseTile(this);

            transform.position = _currentlyOnTile.transform.position;
        }
    }

    public LevelTile CurrentTile {
        get {
            return _currentlyOnTile;
        }
    }

    protected virtual void Awake()
    {
        GameController.Instance.OnTick += OnTick;
    }

    protected virtual void DestroyObject()
    {
        GameController.Instance.OnTick -= OnTick;
    }

    protected virtual void OnTick()
    {

    }
}
