using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private Encounter[] _encounters;

    private int _maxFlowersInBouquete = 3;

    private Ray _ray;
    private RaycastHit _hit;
    private FlowerType _hoveredFlowerType;
    private List<FlowerType> _pickedFlowers = new();

    public event Action<FlowerType> OnHoverOverFlower;
    public event Action<FlowerType, bool> OnFlowerMoved;

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit))
        {
            _hoveredFlowerType = _hit.transform.GetComponentInParent<Flower>().Type;
            if (Input.GetMouseButtonDown(0))
            {
                if (_pickedFlowers.Contains(_hoveredFlowerType))
                {
                    _pickedFlowers.Remove(_hoveredFlowerType);
                    OnFlowerMoved?.Invoke(_hoveredFlowerType, false);
                }
                else if (_pickedFlowers.Count < _maxFlowersInBouquete)
                {
                    _pickedFlowers.Add(_hoveredFlowerType);
                    OnFlowerMoved?.Invoke(_hoveredFlowerType, true);
                }
            }
            else
                OnHoverOverFlower?.Invoke(_hoveredFlowerType);
        }
    }
}

public enum FlowerType
{
    Rose,
    Tulip,
    Lilac,
    Lily,
    Daisy,
    Chrysanthemen,
    Daffodil,
    Hyacinth,
    Marigold,
    Orchid,
    Sunflower,
    Iris
}