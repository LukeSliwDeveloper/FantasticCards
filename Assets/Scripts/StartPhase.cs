using UnityEngine;
using UnityEngine.UI;

public class StartPhase : GamePhase
{
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(End);
    }
}
