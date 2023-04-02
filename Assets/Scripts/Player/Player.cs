using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Player
{
    public string _nickName;
    public Guid _playerID;

    public Player(string nickName)
    {
        _nickName = nickName;
        _playerID = Guid.NewGuid();
    }
}
