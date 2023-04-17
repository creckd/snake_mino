using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private bool _alive = false;
    private EDirection _currentDirection = EDirection.Right;
    private EDirection _directionTravelledLastTick = EDirection.Right;
    private int _currentSize = 3;
    private LinkedList<SnakeBodyPart> _bodyParts = new LinkedList<SnakeBodyPart>();
    private int _applesEatenQueue = 0;
    private Color _myColor;

    private SnakeBodyPart SnakeHead { get { return _bodyParts.First.Value; } }

    public void Initialize(Vector2Int headPosition, ESnakeSpawnOrientationType spawnDirection, int size)
    {
        _alive = true;
        _currentSize = 3;
        _currentDirection = spawnDirection == ESnakeSpawnOrientationType.Horizontal ? EDirection.Right : EDirection.Down;

        // Give the body parts some fun color
        int numberOfColorsAvailable = Databases.Instance.GetConfigDatabase().snakeColorPalette.Length;
        Color randomColor = Databases.Instance.GetConfigDatabase().snakeColorPalette[UnityEngine.Random.Range(0, numberOfColorsAvailable)];
        _myColor = randomColor;

        // Create body parts
        for (int i = 0; i < size; i++)
        {
            // World space does not matter here, since Position's gonna align everything in base class

            SnakeBodyPart bodyPart = Instantiate(Databases.Instance.GetResourceDatabase()._snakeBodyPartPrefab, transform);
            bodyPart._spriteRenderer.color = _myColor;
            bodyPart.SetOwnerSnake(this);

            // Position snake body parts either horizontally or vertically
            Vector2Int bodyPartPosition = spawnDirection == ESnakeSpawnOrientationType.Horizontal ? new Vector2Int(headPosition.x - i, headPosition.y) : new Vector2Int(headPosition.x, headPosition.y + i);
            bodyPart.Position = bodyPartPosition;

            _bodyParts.AddLast(bodyPart);
        }

        GameController.Instance.OnTick += Move;
    }

    public void ChangeDirections(EDirection newDirection)
    {
        bool directionsFacingAway = Vector2.Dot(DirectionToVector2Int(newDirection), DirectionToVector2Int(_directionTravelledLastTick)) == -1;

        // We can't do a full 180
        if (directionsFacingAway)
            return;

        _currentDirection = newDirection;
    }

    private void Move()
    {
        if (!_alive)
            return;

        Vector2Int previousPosition = SnakeHead.Position;
        Vector2Int newPosition = TransformTowardsDirection(SnakeHead.Position, _currentDirection, 1);
        Vector2Int tailPosition = _bodyParts.Last.Value.Position;

        // If we hit a wall, die
        if (LevelController.Instance.GetTileAtPos(newPosition.x, newPosition.y) == null)
        {
            Die();
            return;
        }

        // Otherwise move head
        SnakeHead.Position = TransformTowardsDirection(SnakeHead.Position, _currentDirection, 1);

        // Drag other body parts behind
        for (var part = _bodyParts.First.Next; part != null; part = part.Next) {
            Vector2Int currentPos = part.Value.Position;
            part.Value.Position = previousPosition;
            previousPosition = currentPos;
        }

        // If there is an apple in our stomach, time to grow!
        if (_applesEatenQueue > 0)
        {
            SnakeBodyPart bodyPart = Instantiate(Databases.Instance.GetResourceDatabase()._snakeBodyPartPrefab, transform);
            bodyPart.SetOwnerSnake(this);
            bodyPart._spriteRenderer.color = _myColor;

            bodyPart.Position = tailPosition;
            _bodyParts.AddLast(bodyPart);

            _currentSize++;
            _applesEatenQueue--;
        }

        _directionTravelledLastTick = _currentDirection;
    }

    private Vector2Int TransformTowardsDirection(Vector2Int position, EDirection direction, int amount)
    {
        return position + DirectionToVector2Int(direction) * amount;
    }

    private Vector2Int DirectionToVector2Int(EDirection direction) {
        Vector2Int dir;

        switch (direction)
        {
            case EDirection.Up:
                dir = new Vector2Int(0, 1);
                break;
            case EDirection.Right:
                dir = new Vector2Int(1, 0);
                break;
            case EDirection.Down:
                dir = new Vector2Int(0, -1);
                break;
            case EDirection.Left:
                dir = new Vector2Int(-1, 0);
                break;
            default:
                dir = Vector2Int.zero;
                break;
        }

        return dir;
    }

    public void Grow()
    {
        _applesEatenQueue++;
    }

    public void Die()
    {
        _alive = false;

        var bodypart = _bodyParts.First;
        for (int i = 0; i < _currentSize; i++)
        {
            var objectToDestroy = bodypart.Value;
            Destroy(objectToDestroy);
            bodypart = bodypart.Next;
        }
        Destroy(this.gameObject);
    }
}
