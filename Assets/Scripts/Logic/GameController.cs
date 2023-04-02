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
    }

    public void StartGame(ConfigDatabaseScriptableObject config)
    {
        _currentGamestate = EGameState.InGame;

        // Create world

        LevelController.Instance.CreateEmptyLevel(config);

        // Spawn snakes for each player

        List<Player> players = PlayerManager.Instance.CurrentPlayers;
        for (int i = 0; i < players.Count; i++)
        {
            SnakeSpawnPoint[] defaultSpawnPoints = config._snakeSpawnPoints;

            Player player = players[i];
            Snake newSnake = SnakeManager.Instance.CreateSnake(defaultSpawnPoints[UnityEngine.Random.Range(0, defaultSpawnPoints.Length)], config._startingCharacterSize);

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
        _deltaTimeSinceLastTick += Time.deltaTime;

        if (_deltaTimeSinceLastTick >= _tickFrequency)
        {
            _deltaTimeSinceLastTick = 0f;
            Tick();
        }
    }
}
