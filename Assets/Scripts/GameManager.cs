using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private GamePhase[] _gamePhases;
    [SerializeField] private MatchData _matchData;

    private int _currentPhaseIndex;

    protected override void Initialize()
    {
        if (!_wasInitialized)
        {
            foreach (var phase in _gamePhases)
                phase.OnEnded += BeginNextPhase;
            _gamePhases[_currentPhaseIndex].Begin(_matchData);
        }
        base.Initialize();
    }

    private void BeginNextPhase()
    {
        _gamePhases[_currentPhaseIndex + 1].Begin(_gamePhases[_currentPhaseIndex].Data);
        _currentPhaseIndex++;
    }
}
