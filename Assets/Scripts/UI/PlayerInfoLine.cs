using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoLine : MonoBehaviour
{
    public TextMeshProUGUI _playerNameField;
    public TextMeshProUGUI _inputKey1Text;
    public TextMeshProUGUI _inputKey2Text;
    public TextMeshProUGUI _inputKey3Text;
    public TextMeshProUGUI _inputKey4Text;

    public void Setup(string playerName, EInputLayout keyboardLayout)
    {
        _playerNameField.text = playerName;

        switch (keyboardLayout)
        {
            case EInputLayout.Arrows:
                _inputKey1Text.text = "Up";
                _inputKey2Text.text = "Left";
                _inputKey3Text.text = "Down";
                _inputKey4Text.text = "Right";
                break;
            case EInputLayout.WASD:
                _inputKey1Text.text = "W";
                _inputKey2Text.text = "A";
                _inputKey3Text.text = "S";
                _inputKey4Text.text = "D";
                break;
        }
    }

}
