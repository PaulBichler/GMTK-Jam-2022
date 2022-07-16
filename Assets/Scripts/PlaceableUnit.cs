using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceableUnit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isDragging;

    private void Update()
    {
        if (_isDragging)
            transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print(name + " --> Down");
        _isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print(name + " --> Up");
        _isDragging = false;
    }
}
