using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyPart : LevelObject
{
    private Snake _mySnake;

    public void SetOwnerSnake(Snake snake)
    {
        _mySnake = snake;
    }
}
