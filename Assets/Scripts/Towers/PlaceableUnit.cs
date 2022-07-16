using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceableUnit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isDragging;

    protected virtual void Update()
    {
        if (_isDragging)
            transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
    }

    public virtual void OnPlace()
    {
        
    }
}
