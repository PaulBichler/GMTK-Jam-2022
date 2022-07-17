using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceableUnit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isDragging;
    private bool _isPlaced;

    protected virtual void Update()
    {
        if (_isDragging)
            transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(_isPlaced) return;
        
        _isDragging = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        OnPlace();
    }

    public virtual void OnPlace()
    {
        _isPlaced = true;
    }
}
