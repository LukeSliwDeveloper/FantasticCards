using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RivalCodePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _loadButton;

    private int _codeLength = 7;

    public event Action<string> OnCodeLoaded;

    private void Awake()
    {
        _inputField.onValueChanged.AddListener(InputTextToUpper);
        _loadButton.onClick.AddListener(LoadRivalBoard);
    }

    private void InputTextToUpper(string t) => _inputField.text = t.ToUpper();

    private void LoadRivalBoard()
    {
        if (_inputField.text.Length == _codeLength)
        {
            OnCodeLoaded?.Invoke(_inputField.text);
            Destroy(gameObject);
        }
    }
}
