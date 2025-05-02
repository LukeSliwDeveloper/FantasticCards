using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DraftPhase : GamePhase
{
    private int _cardsDraftedPerDeck = 18;
    private Vector2 _cardSpawnPosition = Vector2.left * 1.4f;

    private Card[] _draftedPair = new Card[2];
    private int _cardsOnBoardCount;
    private int _cardDraftCodeNumber;
    private List<Card> playerCards;

    public override void Begin(MatchData data)
    {
        base.Begin(data);
        Random.InitState(data.MatchKey);
        List<Card>[] contestantsDecks = new List<Card>[data.ContestantsNames.Count];
        for (int i = 0; i < contestantsDecks.Length; i++)
            contestantsDecks[i] = new();
        List<int> allCardsIndexes = new();
        for (int i = 0; i < data.AllCards.Length; i++)
            allCardsIndexes.Add(i);
        int randomIndex;
        int pickedCardIndex;
        int cardsDraftedTotal = 0;
        int currentlyDrawingContestant = 0;
        while(cardsDraftedTotal < data.ContestantsNames.Count * _cardsDraftedPerDeck)
        {
            randomIndex = Random.Range(0, allCardsIndexes.Count);
            pickedCardIndex = allCardsIndexes[randomIndex];
            allCardsIndexes.RemoveAt(randomIndex);
            contestantsDecks[currentlyDrawingContestant].Add(data.AllCards[pickedCardIndex]);
            currentlyDrawingContestant = (currentlyDrawingContestant + 1) % data.ContestantsNames.Count;
            cardsDraftedTotal++;
        }
        Data.ContestantsDecks = contestantsDecks;
        playerCards = contestantsDecks[Data.PlayerIndex];
        Data.ContestantsBoards = new Dictionary<CardPosition, Card>[data.ContestantsNames.Count];
        for (int i = 0; i < Data.ContestantsBoards.Length; i++)
            Data.ContestantsBoards[i] = new();
        SpawnDraftPair();
    }

    private void SpawnDraftPair(Card previouslyPickedCard = null)
    {
        if (previouslyPickedCard != null)
        {
            foreach (var card in _draftedPair)
                card.OnClicked -= SpawnDraftPair;
            if (_draftedPair[0] == previouslyPickedCard)
                Destroy((_draftedPair[1]).gameObject);
            else
            {
                _cardDraftCodeNumber = _cardDraftCodeNumber | 1 << (_cardsOnBoardCount % 5);
                Destroy((_draftedPair[0]).gameObject);
            }
            if (_cardsOnBoardCount == 4)
            {
                Data.DraftCode = Data.EncodingLetters[_cardDraftCodeNumber].ToString();
                _cardDraftCodeNumber = 0;
            }
            var cardPosition = (CardPosition)_cardsOnBoardCount;
            previouslyPickedCard.InitializeToPosition(cardPosition, Data.GetWorldCardPosition(cardPosition, Data.PlayerIndex), Data.PlayerIndex);
            Data.ContestantsBoards[Data.PlayerIndex].Add(cardPosition, previouslyPickedCard);
            if (++_cardsOnBoardCount == 9)
            {
                Data.DraftCode += Data.EncodingLetters[_cardDraftCodeNumber];
                End();
                return;
            }
        }
        _draftedPair[0] = Instantiate(playerCards[_cardsOnBoardCount * 2], _cardSpawnPosition, Quaternion.identity);
        _draftedPair[1] = Instantiate(playerCards[_cardsOnBoardCount * 2 + 1], -_cardSpawnPosition, Quaternion.identity);
        foreach (var card in _draftedPair)
            card.OnClicked += SpawnDraftPair;
    }
}
