using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupPhase : GamePhase
{
    [SerializeField] private TMP_InputField _kingdomInputField;
    [SerializeField] private TMP_InputField _playerInputField;
    [SerializeField] private Button _addRivalButton;
    [SerializeField] private Button _removeRivalButton;
    [SerializeField] private Transform _rivalInputFieldsContainer;
    [SerializeField] private Button _nextButton;
    [SerializeField] private TMP_InputField _inputFieldPrefab;
    [SerializeField] private List<TMP_InputField> _rivalInputFields;

    private int _maxRivalsCount = 4;

    private void Awake()
    {
        _nextButton.onClick.AddListener(End);
        _addRivalButton.onClick.AddListener(AddRivalInputField);
        _removeRivalButton.onClick.AddListener(RemoveRivalInputField);
    }

    protected override void End()
    {
        if (_kingdomInputField.text != "" && _playerInputField.text != "" && _rivalInputFields.Count != 0 && !_rivalInputFields.Exists((x) => x.text == ""))
        {
            var contestantsNames = new List<string>();
            contestantsNames.Add(_playerInputField.text);
            foreach (var rivalField in _rivalInputFields)
                contestantsNames.Add(rivalField.text);
            contestantsNames.Sort();
            var matchKeyText = _kingdomInputField.text;
            foreach (var playerName in contestantsNames)
                matchKeyText += playerName;
            Data.KingdomName = _kingdomInputField.text;
            Data.PlayerName = _playerInputField.text;
            Data.ContestantsNames = contestantsNames;
            Data.PlayerIndex = Data.ContestantsNames.IndexOf(Data.PlayerName);
            Data.MatchKey = Animator.StringToHash(matchKeyText);
            base.End();
        }
    }

    private void AddRivalInputField()
    {
        if (_rivalInputFields.Count < _maxRivalsCount)
            _rivalInputFields.Add(Instantiate(_inputFieldPrefab, _rivalInputFieldsContainer));
    }

    private void RemoveRivalInputField()
    {
        Destroy(_rivalInputFields[_rivalInputFields.Count - 1].gameObject);
        _rivalInputFields.RemoveAt(_rivalInputFields.Count - 1);
    }
}
