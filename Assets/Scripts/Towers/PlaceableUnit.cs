using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlaceableUnit : MonoBehaviour, IPointerDownHandler
{
    private bool _isPlacing;
    private bool _isPlaced;

    public UnityEvent OnPlaced { get; } = new();

    protected virtual void Update()
    {
        if (_isPlacing)
            transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (_isPlaced) return;
        
        if (_isPlacing)
        {
            OnPlace();
            return;
        }

        _isPlacing = true;
    }

    public virtual void OnPlace()
    {
        _isPlaced = true;
        _isPlacing = false;
        OnPlaced?.Invoke();
    }
}
