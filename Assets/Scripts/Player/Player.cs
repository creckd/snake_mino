using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player
{
    public string _nickName;
    public Guid _playerID;
    public EInputLayout _controls;

    public Player(string nickName, EInputLayout controls)
    {
        _nickName = nickName;
        _playerID = Guid.NewGuid();
        _controls = controls;

        SetupControls(controls);
    }

    private void SetupControls(EInputLayout layout)
    {
        InputController.Instance.RegisterInput(layout, this);
    }

    public void SendInputDirection(EDirection direction) {
        GameController.Instance.OnPlayerInput(this, direction);
    }

    public void OnDestroy()
    {
        InputController.Instance.UnRegisterInput(this);
    }
}
