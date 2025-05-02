using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollInputFieldFixer : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    private ScrollRect _scrollRect = null;
    private TMP_InputField _input = null;
    private bool _isDragging = false;


    private void Start()
    {
        _scrollRect = GetComponentInParent<ScrollRect>();
        _input = GetComponent<TMP_InputField>();
        _input.DeactivateInputField();
        _input.enabled = false;
    }


    public void OnBeginDrag(PointerEventData data)
    {
        if (_scrollRect != null && _input != null)
        {
            _isDragging = true;
            _input.DeactivateInputField();
            _input.enabled = false;
            _scrollRect.SendMessage("OnBeginDrag", data);
        }
    }


    public void OnEndDrag(PointerEventData data)
    {
        if (_scrollRect != null && _input != null)
        {
            _isDragging = false;
            _scrollRect.SendMessage("OnEndDrag", data);
        }
    }


    public void OnDrag(PointerEventData data)
    {
        if (_scrollRect != null && _input != null)
        {
            _scrollRect.SendMessage("OnDrag", data);
        }
    }


    public void OnPointerClick(PointerEventData data)
    {
        if (_scrollRect != null && _input != null)
        {
            if (!_isDragging && !data.dragging)
            {
                _input.enabled = true;
                _input.ActivateInputField();
            }
        }
    }
}