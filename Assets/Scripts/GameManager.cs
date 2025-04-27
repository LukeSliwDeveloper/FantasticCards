using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private GameState[] _gameStates;

    private Vector2 _positionOffsetPerPlayer = Vector2.right * 1.125f;

    private Dictionary<BoardPosition, Vector2> _boardPositions = new()
    {
        [BoardPosition.UpFront] = new Vector2(1.8f, 2.5f),
        [BoardPosition.MidFront] = new Vector2(1.8f, 0f),
        [BoardPosition.DownFront] = new Vector2(1.8f, -2.5f),
        [BoardPosition.UpCenter] = new Vector2(0f, 2.5f),
        [BoardPosition.MidCenter] = new Vector2(0f, 0f),
        [BoardPosition.DownCenter] = new Vector2(0f, -2.5f),
        [BoardPosition.UpBack] = new Vector2(-1.8f, 2.5f),
        [BoardPosition.MidBack] = new Vector2(-1.8f, 0f),
        [BoardPosition.DownBack] = new Vector2(-1.8f, -2.5f),
    };

    private int _currentGameStateIndex = -1;

    private Dictionary<int, Card[]> _cardsOnBoard = new();

    public Vector2 BoardToRealPosition(BoardPosition boardPosition, int playerIndex) => _boardPositions[boardPosition] + _positionOffsetPerPlayer * playerIndex;

    public void CardAddedToBoard(int playerIndex, Card card, BoardPosition boardPosition) 
    {
        if (!_cardsOnBoard.ContainsKey(playerIndex))
            _cardsOnBoard.Add(playerIndex, new Card[9]);
        _cardsOnBoard[playerIndex][(int)boardPosition] = card;
    }

    public Card[] GetCardsOfPlayer(int playerIndex) => _cardsOnBoard[playerIndex];

    protected override void Initialize()
    {
        if (!_wasInitialized)
        {
            foreach (var gameState in _gameStates)
                gameState.Initialize(GotToNextGameState);
            GotToNextGameState();
        }
        base.Initialize();
    }

    private void GotToNextGameState()
    {
        _gameStates[_currentGameStateIndex = (_currentGameStateIndex + 1) % _gameStates.Length].Activate();
    }
}
