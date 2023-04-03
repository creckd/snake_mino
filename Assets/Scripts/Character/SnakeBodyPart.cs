using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyPart : LevelObject
{
    public SpriteRenderer _spriteRenderer;

    private Snake _mySnake;

    public void SetOwnerSnake(Snake snake)
    {
        _mySnake = snake;
    }

    public Snake GetOwnerSnake()
    {
        return _mySnake;
    }
}
