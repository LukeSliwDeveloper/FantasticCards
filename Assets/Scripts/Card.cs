using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField] private Canvas _canvas;

    private Vector3 _onBoardScale = Vector3.one * .35f;
    private float _exitDraftTime = .3f;

    private BoardPosition _position;
    private CardPhase _phase;

    private BoardPosition Position
    {
        get => _position;
        set
        {
            _position = value;
            _sortingGroup.sortingOrder = _canvas.sortingOrder = (int)value;
        }
    }

    public event Action OnClicked;

    public void Initialize(BoardPosition position)
    {
        Position = position;
    }

    public void ActivatePhase(CardPhase phase)
    {
        _phase = phase;
    }

    public void HideFromDraft(Vector2 hidePosition)
    {
        if(_phase == CardPhase.Draft)
        {
            _phase = CardPhase.Inactive;
            transform.DOMove(hidePosition, _exitDraftTime).OnComplete(() => Destroy(gameObject));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_phase == CardPhase.Draft)
        {
            _phase = CardPhase.Inactive;
            transform.DOScale(_onBoardScale, _exitDraftTime);
            transform.DOMove(GameManager.Instance.BoardToRealPosition(_position, 0), _exitDraftTime);
            GameManager.Instance.CardAddedToBoard(0, this, _position);
            OnClicked?.Invoke();
        }
    }
}

public enum BoardPosition
{
    UpFront,
    MidFront,
    DownFront,
    UpCenter,
    MidCenter,
    DownCenter,
    UpBack,
    MidBack,
    DownBack
}

public enum CardPhase
{
    Inactive,
    Draft,
    Arrange,
    Fight
}