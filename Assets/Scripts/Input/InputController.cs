using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private static InputController instance = null;
    public static InputController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<InputController>();
            return instance;
        }
    }

    private Dictionary<EInputLayout, Player> _registeredInputs = new Dictionary<EInputLayout, Player>();

    public void RegisterInput(EInputLayout layout, Player player)
    {
        if (!_registeredInputs.ContainsKey(layout))
        {
            _registeredInputs.Add(layout, player);
        }
    }

    public void UnRegisterInput(Player player)
    {
        _registeredInputs.Remove(player._controls);
    }

    private void Update()
    {
        if (_registeredInputs.ContainsKey(EInputLayout.WASD))
        {

            Player p = _registeredInputs[EInputLayout.WASD];

            if (Input.GetKeyDown(KeyCode.W))
            {
                p.SendInputDirection(EDirection.Up);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                p.SendInputDirection(EDirection.Left);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                p.SendInputDirection(EDirection.Down);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                p.SendInputDirection(EDirection.Right);
            }
        }

        if (_registeredInputs.ContainsKey(EInputLayout.Arrows))
        {

            Player p = _registeredInputs[EInputLayout.Arrows];

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                p.SendInputDirection(EDirection.Up);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                p.SendInputDirection(EDirection.Left);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                p.SendInputDirection(EDirection.Down);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                p.SendInputDirection(EDirection.Right);
            }
        }

    }
}
