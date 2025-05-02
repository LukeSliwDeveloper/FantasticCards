using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePhase : MonoBehaviour
{
    public MatchData Data { get; protected set; }

    public event Action OnEnded;

    public virtual void Begin(MatchData data)
    {
        Data = data;
        gameObject.SetActive(true);
    }

    protected virtual void End()
    {
        gameObject.SetActive(false);
        OnEnded?.Invoke();
    }
}
