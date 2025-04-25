using System;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] private GameSettings _settings;

    public Action OnChangeState;

    public void Initialize(Action onChangeState)
    {
        OnChangeState = onChangeState;
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }
}
