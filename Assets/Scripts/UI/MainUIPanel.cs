using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIPanel : MonoBehaviour
{
    public Button _playButton;
    public TMP_InputField _playerNameInputField;

    public PlayerInfoLine _playerInfoLineSample;

    public void Initialize()
    {
        _playerInfoLineSample.gameObject.SetActive(false);
    }

    public void AddPlayerButton()
    {
        if (string.IsNullOrEmpty(_playerNameInputField.text))
            return;

        // Only hardcoded 2 different input layouts for now
        EInputLayout layoutToUse = PlayerManager.Instance.CurrentPlayers.Count > 0 ? EInputLayout.WASD : EInputLayout.Arrows;
        Player p = PlayerManager.Instance.AddPlayer(_playerNameInputField.text, layoutToUse);

        PlayerInfoLine playerInfoLine = Instantiate(_playerInfoLineSample, _playerInfoLineSample.transform.parent.transform) as PlayerInfoLine;
        playerInfoLine.Setup(_playerNameInputField.text, p._controls);
        playerInfoLine.gameObject.SetActive(true);

        _playerNameInputField.text = "";
        RefreshPlayButtonState();
    }

    public void PlayButton()
    {
        PlayerManager.Instance.OnEveryoneReady();
    }

    private void RefreshPlayButtonState()
    {
        _playButton.interactable = PlayerManager.Instance.CurrentPlayers.Count > 0;
    }
}
