using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = null;
    public static PlayerManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<PlayerManager>();
            return instance;
        }
    }

    public Action OnEveryoneReady = delegate { };

    private List<Player> _currentPlayers = new List<Player>();
    private Dictionary<Player, bool> _readyCheck = new Dictionary<Player, bool>();

    public List<Player> CurrentPlayers { get { return _currentPlayers; } }

    public void Initialize()
    {

    }

    private void Start()
    {
        Guid firstPlayer = AddPlayer("Dendi");
        SetPlayerReady(firstPlayer, true);
    }

    private void ReadyCheck()
    {
        bool allReady = true;
        foreach (var kvp in _readyCheck) {
            if (!kvp.Value)
            {
                allReady = false;
                break;
            }
        }

        if (allReady)
            OnEveryoneReady();
    }

    private Guid AddPlayer(string name)
    {
        Player player = new Player(name);

        _currentPlayers.Add(player);
        _readyCheck.Add(player, false);

        return player._playerID;
    }

    private void SetPlayerReady(Guid playerID, bool isReady)
    {
        _readyCheck[GetPlayerByGuid(playerID)] = isReady;

        ReadyCheck();
    }

    public Player GetPlayerByGuid(Guid id)
    {
        for (int i = 0; i < _currentPlayers.Count; i++)
        {
            if (_currentPlayers[i]._playerID == id)
                return _currentPlayers[i];
        }

        throw new Exception("Cant find player with this Guid");
    }

    public Guid GetPlayerIdByName(string name)
    {
        for (int i = 0; i < _currentPlayers.Count; i++)
        {
            if (_currentPlayers[i]._nickName == name)
                return _currentPlayers[i]._playerID;
        }

        throw new Exception("Cant find player with this name");
    }
}
