using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    private static SnakeManager instance = null;
    public static SnakeManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<SnakeManager>();
            return instance;
        }
    }

    public Snake CreateSnake(SnakeSpawnPoint spawnPosition, int snakeStartingSize)
    {
        Snake snake = Instantiate(Databases.Instance.GetResourceDatabase()._snakePrefab, transform) as Snake;

        snake.Initialize(spawnPosition._headPosition, spawnPosition._spawnOrientationType, snakeStartingSize);
    
        return snake;
    }
}
