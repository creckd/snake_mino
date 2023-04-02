using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelData
{
    private int _width;
    private int _height;

    private List<LevelObject> _objects;

    public LevelData(int width, int height, List<LevelObject> objects = null)
    {
        this._width = width;
        this._height = height;
        this._objects = objects;
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public List<LevelObject> GetObjects()
    {
        return _objects;
    }
}
