using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MatchData : ScriptableObject
{
    [field:SerializeField] public Card[] AllCards { get; private set; }

    [HideInInspector] public Vector2 BoardOffsetPerContestant = Vector2.right * 5.625f;

    [HideInInspector] public List<char> EncodingLetters = new()
    {
        '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'U', 'W', 'Y', 'Z'
    };

    public Dictionary<CardPosition, Vector2> WorldCardPositions { get; private set; } = new()
    {
        { CardPosition.FrontUp, new Vector2(1.8f, 2.3f) },
        { CardPosition.FrontMiddle, new Vector2(1.8f, 0f) },
        { CardPosition.FrontDown, new Vector2(1.8f, -2.3f) },
        { CardPosition.MiddleUp, new Vector2(0f, 2.3f) },
        { CardPosition.MiddleMiddle, new Vector2(0f, 0f) },
        { CardPosition.MiddleDown, new Vector2(0f, -2.3f) },
        { CardPosition.BackUp, new Vector2(-1.8f, 2.3f) },
        { CardPosition.BackMiddle, new Vector2(-1.8f, 0f) },
        { CardPosition.BackDown, new Vector2(-1.8f, -2.3f) }
    };

    [HideInInspector] public string KingdomName;
    [HideInInspector] public string PlayerName;
    [HideInInspector] public List<string> ContestantsNames;
    [HideInInspector] public int PlayerIndex;
    [HideInInspector] public int MatchKey;
    [HideInInspector] public List<Card>[] ContestantsDecks;
    [HideInInspector] public string DraftCode;
    [HideInInspector] public Dictionary<CardPosition, Card>[] ContestantsBoards;


    public Vector2 GetWorldCardPosition(CardPosition position, int contestantIndex)
    {
        return WorldCardPositions[position] + Helper.Mod(contestantIndex - PlayerIndex, ContestantsNames.Count) * BoardOffsetPerContestant;
    }

    private void OnValidate()
    {
        for (int i = 0; i < AllCards.Length; i++)
            AllCards[i].SetIndex(i);
    }
}