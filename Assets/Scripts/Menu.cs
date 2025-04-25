using System;
using UnityEngine;

public class Menu : GameState
{
    [SerializeField] private Animator _animator;

    private int _animatorHash = Animator.StringToHash("Visible");

    public override void Activate()
    {
        base.Activate();
        _animator.SetBool(_animatorHash, true);
    }

    public void Hide()
    {
        _animator.SetBool(_animatorHash, false);
    }

    private void GotToNextGameState()
    {
        OnChangeState?.Invoke();
        gameObject.SetActive(false);
    }
}
