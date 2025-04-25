using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : GameState
{
    [SerializeField] private SpritesContainer[] _characterSprites;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image[] _characterImages;
    [SerializeField] private Background _background;

    private int _animatorHash = Animator.StringToHash("Visible");

    public override void Activate()
    {
        base.Activate();
        for (int i = 0; i < _characterImages.Length; i++)
        {
            _characterImages[i].sprite = _characterSprites[i].Sprites[UnityEngine.Random.Range(0, _characterSprites[i].Sprites.Length)];
            _characterImages[i].SetNativeSize();
        }
        _animator.SetBool(_animatorHash, true);
        _background.ScrollRandomly();
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

[Serializable]
public struct SpritesContainer
{
    [field:SerializeField] public Sprite[] Sprites { get; private set; }
}