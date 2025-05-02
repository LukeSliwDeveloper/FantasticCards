using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviourSingleton<CameraManager>, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private MatchData _matchdData;

    private float _previousDragX;

    public Vector2 ToWorld(Vector2 screenPosition) => _camera.ScreenToWorldPoint(screenPosition);

    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousDragX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _previousDragX = eventData.position.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
