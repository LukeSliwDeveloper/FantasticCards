using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TacticsPhase : GamePhase
{
    [SerializeField] private TMP_Text _playerCodeText;
    [SerializeField] private Button _nextButton;
    [SerializeField] private RivalCodePanel _rivalCodePanelPrefab;

    private int _tacticsCodeLength = 5;

    private Vector2 _dragOffset;
    private Card _draggedCard;

    private void Awake()
    {
        _nextButton.onClick.AddListener(End);
    }

    public override void Begin(MatchData data)
    {
        base.Begin(data);
        GenerateCode();
        RivalCodePanel panel;
        for (int i = 1; i < Data.ContestantsNames.Count; i++)
        {
            panel = Instantiate(_rivalCodePanelPrefab, Data.BoardOffsetPerContestant * i, Quaternion.identity);
            var index = i;
            panel.OnCodeLoaded += (c) => LoadRivalCode((index + Data.PlayerIndex) % Data.ContestantsNames.Count, c);
        }
        foreach (var playerCard in data.ContestantsBoards[data.PlayerIndex])
        {
            playerCard.Value.OnDragBegan += StartDraggingCard;
            playerCard.Value.OnDragged += DragCard;
            playerCard.Value.OnDragEnded += StopDraggingCard;
        }
    }

    protected override void End()
    {
        foreach (var playerCard in Data.ContestantsBoards[Data.PlayerIndex])
        {
            playerCard.Value.OnDragBegan -= StartDraggingCard;
            playerCard.Value.OnDragged -= DragCard;
            playerCard.Value.OnDragEnded -= StopDraggingCard;
        }
        base.End();
    }

    private void GenerateCode()
    {
        var playerDeck = new List<Card>(Data.ContestantsDecks[Data.PlayerIndex]);
        var playerBoard = Data.ContestantsBoards[Data.PlayerIndex];
        int[] codeNumbers = new int[_tacticsCodeLength];
        var tacticsCode = "";
        for (int i = 0; i < _tacticsCodeLength; i++)
        {
            codeNumbers[i] = playerDeck.IndexOf(playerDeck.First((c) => c.Index == playerBoard[(CardPosition)i].Index)) / 2;
            playerDeck.RemoveAt(codeNumbers[i] * 2);
            playerDeck.RemoveAt(codeNumbers[i] * 2);
        }
        tacticsCode += Data.EncodingLetters[codeNumbers[0]];
        for (int i = 1; i < _tacticsCodeLength; i++)
        {
            codeNumbers[i] = (codeNumbers[i] << 2);
            codeNumbers[i] += playerDeck.IndexOf(playerDeck.First((c) => c.Index == playerBoard[(CardPosition)(_tacticsCodeLength + i - 1)].Index)) / 2;
            tacticsCode += Data.EncodingLetters[codeNumbers[i]];
        }
        _playerCodeText.text = Data.DraftCode + tacticsCode;
    }

    private void LoadRivalCode(int contestantIndex, string code)
    {
        var rivalDeck = new List<Card>(Data.ContestantsDecks[contestantIndex]);
        var rivalPickedCards = new List<Card>();
        var rivalBoard = Data.ContestantsBoards[contestantIndex];
        int codeBit = Data.EncodingLetters.IndexOf(code[0]);
        code = code.Substring(1, code.Length - 1);
        for (int i = 0; i < 5; i++)
        {
            rivalPickedCards.Add(rivalDeck[(((codeBit & 1) == 1) ? i * 2 + 1 : i * 2)]);
            codeBit = codeBit >> 1;
        }
        codeBit = Data.EncodingLetters.IndexOf(code[0]);
        code = code.Substring(1, code.Length - 1);
        for (int i = 5; i < 9; i++)
        {
            rivalPickedCards.Add(rivalDeck[(((codeBit & 1) == 1) ? i * 2 + 1 : i * 2)]);
            codeBit = codeBit >> 1;
        }
        for (int i = 0; i < _tacticsCodeLength; i++)
        {
            codeBit = Data.EncodingLetters.IndexOf(code[i]);
            if (i > 0)
                codeBit = codeBit >> 2;
            rivalBoard[(CardPosition)i] = Instantiate(rivalPickedCards[codeBit]);
            rivalPickedCards.RemoveAt(codeBit);
        }
        for (int i = 1; i < _tacticsCodeLength; i++)
        {
            codeBit = Data.EncodingLetters.IndexOf(code[i]);
            codeBit = codeBit & 3;
            rivalBoard[(CardPosition)(_tacticsCodeLength + i - 1)] = Instantiate(rivalPickedCards[codeBit]);
        }
        foreach (var rivalCard in rivalBoard)
            rivalCard.Value.InitializeToPosition(rivalCard.Key, Data.GetWorldCardPosition(rivalCard.Key, contestantIndex), contestantIndex);
    }

    private void StartDraggingCard(Card card, Vector2 position)
    {
        if (_draggedCard == null)
        {
            _draggedCard = card;
            _dragOffset = (Vector2)card.transform.position - position;
        }
    }

    private void DragCard(Card card, Vector2 position)
    {
        if (card == _draggedCard)
            card.MoveToWorldPosition(position + _dragOffset);
    }

    private void StopDraggingCard(Card card, Vector2 position)
    {
        if (card == _draggedCard)
        {
            card.MoveToWorldPosition(position + _dragOffset);
            float minimalDistance = float.MaxValue;
            float currentDistance;
            CardPosition positionToSwapWith = card.Position;
            foreach (var worldCardPosition in Data.WorldCardPositions)
            {
                currentDistance = (worldCardPosition.Value - (Vector2)card.transform.position).sqrMagnitude;
                if (currentDistance < minimalDistance)
                {
                    minimalDistance = currentDistance;
                    positionToSwapWith = worldCardPosition.Key;
                }
            }
            if (positionToSwapWith != card.Position)
            {
                var swappedCard = Data.ContestantsBoards[Data.PlayerIndex][positionToSwapWith];
                swappedCard.MoveToPosition(card.Position, Data.GetWorldCardPosition(card.Position, Data.PlayerIndex));
                Data.ContestantsBoards[Data.PlayerIndex][card.Position] = swappedCard;
            }
            card.MoveToPosition(positionToSwapWith, Data.GetWorldCardPosition(positionToSwapWith, Data.PlayerIndex));
            Data.ContestantsBoards[Data.PlayerIndex][positionToSwapWith] = card;
            _draggedCard = null;
            GenerateCode();
        }
    }
}
