using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlowerInfo : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<FlowerType, string> _infoPerFlower;
    [SerializeField] private TMP_Text _text;

    private bool _resetText;

    private void Awake()
    {
        GameManager.Instance.OnHoverOverFlower += ShowInfo;
    }

    private void ShowInfo(FlowerType type)
    {
        _text.text = _infoPerFlower[type];
        _resetText = false;
    }

    private void Update()
    {
        if (_resetText)
        {
            _text.text = "";
        }
        _resetText = true;
    }
}
