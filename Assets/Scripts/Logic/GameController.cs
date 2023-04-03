using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    WaitingForPlayers,
    InGame,
    GameOver
}

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<GameController>();
            return instance;
        }
    }

    // Tick happens in intervals defined in config, issued by GameController, all LevelObjects subscribe to Tick event by default
    public Action OnTick = delegate { };
    public Action<EGameState> OnGameStateChange = delegate { };
    public Action<LevelObject, LevelObject> OnCollision = delegate { };
    public Action<Player, EDirection> OnPlayerInput = delegate { };

    private EGameState _currentGamestate;
    private float _tickFrequency;
    private float _deltaTimeSinceLastTick = 0f;

    private Dictionary<Player, Snake> _playerToSnake = new Dictionary<Player, Snake>();

    private void Awake()
    {
        // Game flow begins here, at first frame
        InitializeGame(Databases.Instance.GetConfigDatabase());
    }

    public void InitializeGame(ConfigDatabaseScriptableObject config)
    {
        _currentGamestate = EGameState.WaitingForPlayers;
        _tickFrequency = config._tickFrequency;

        // Wait until all players ready before starting the game...
        PlayerManager.Instance.Initialize();
        PlayerManager.Instance.OnEveryoneReady += () => { StartGame(config); };

        OnCollision += OnCollisionHandler;
    }

    public void StartGame(ConfigDatabaseScriptableObject config)
    {
        _currentGamestate = EGameState.InGame;
        OnGameStateChange(_currentGamestate);

        OnPlayerInput += OnPlayerInputHandler;

        // Create world

        LevelController.Instance.CreateEmptyLevel(config);

        // Spawn snakes for each player

        List<Player> players = PlayerManager.Instance.CurrentPlayers;
        for (int i = 0; i < players.Count; i++)
        {
            SnakeSpawnPoint[] defaultSpawnPoints = config._snakeSpawnPoints;

            Player player = players[i];
            Snake newSnake = SnakeManager.Instance.CreateSnake(defaultSpawnPoints[i], config._startingCharacterSize);

            _playerToSnake.Add(player, newSnake);
        }

        // Start the game
    }

    public void Tick()
    {
        OnTick();
    }

    private void Update()
    {
        if (_currentGamestate == EGameState.InGame)
        {
            _deltaTimeSinceLastTick += Time.deltaTime;

            if (_deltaTimeSinceLastTick >= _tickFrequency)
            {
                _deltaTimeSinceLastTick = 0f;
                Tick();
            }
        }
    }

    private void OnPlayerInputHandler(Player p, EDirection dir)
    {
        if (_playerToSnake.ContainsKey(p))
            _playerToSnake[p].ChangeDirections(dir);
    }
    
    // Meaning that first initiated a collision with second
    private void OnCollisionHandler(LevelObject first, LevelObject second)
    {
        if (first is SnakeBodyPart && second is Apple)
        {
            Destroy(second.gameObject);
            (first as SnakeBodyPart).GetOwnerSnake().Grow();
            LevelController.Instance.SpawnAppleAtRandomEmptyTile();
        }
        if (first is Apple && second is SnakeBodyPart)
        {
            Destroy(first.gameObject);
            (second as SnakeBodyPart).GetOwnerSnake().Grow();
            LevelController.Instance.SpawnAppleAtRandomEmptyTile();
        }

        if (first is SnakeBodyPart && second is SnakeBodyPart)
        {
            (first as SnakeBodyPart).GetOwnerSnake().Die();
        }
    }
}
