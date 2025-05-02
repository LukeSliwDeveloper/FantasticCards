using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SortingGroup _sortingGroup;
    [field: SerializeField] public int Index { get; private set; }

    private int _offGridSortingOrder = 0;
    private int _onGridSortingOrder = -1;

    private int _ownerIndex;

    private int SortingOrder
    { 
        set => _canvas.sortingOrder = _sortingGroup.sortingOrder = value;
    }

    public CardPosition Position { get; private set; }

    public event Action<Card> OnClicked;
    public event Action<Card, Vector2> OnDragBegan, OnDragged, OnDragEnded;

    public void InitializeToPosition(CardPosition position, Vector2 worldPosition, int ownerIndex)
    {
        MoveToPosition(position, worldPosition);
        transform.localScale = Vector3.one;
        _ownerIndex = ownerIndex;
    }

    public void MoveToWorldPosition(Vector2 position)
    {
        transform.position = position;
        SortingOrder = _offGridSortingOrder;
    }

    public void MoveToPosition(CardPosition position, Vector2 worldPosition)
    {
        Position = position;
        transform.position = worldPosition;
        SortingOrder = _onGridSortingOrder;
    }

    public void SetIndex(int value) => Index = value;

    public void OnPointerClick(PointerEventData eventData) => OnClicked?.Invoke(this);

    public void OnBeginDrag(PointerEventData eventData) => OnDragBegan?.Invoke(this, CameraManager.Instance.ToWorld(eventData.position));

    public void OnDrag(PointerEventData eventData) => OnDragged?.Invoke(this, CameraManager.Instance.ToWorld(eventData.position));

    public void OnEndDrag(PointerEventData eventData) => OnDragEnded?.Invoke(this, CameraManager.Instance.ToWorld(eventData.position));
}

public enum CardPosition
{
    FrontUp,
    FrontMiddle,
    FrontDown,
    MiddleUp,
    MiddleMiddle,
    MiddleDown,
    BackUp,
    BackMiddle,
    BackDown
}