using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController instance = null;
    public static UIController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<UIController>();
            return instance;
        }
    }

    public MainUIPanel _mainPanel;

    private void Awake()
    {
        _mainPanel.Initialize();
    }

    private void Start()
    {
        GameController.Instance.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(EGameState state)
    {
        _mainPanel.gameObject.SetActive(state == EGameState.WaitingForPlayers);
    }
}
