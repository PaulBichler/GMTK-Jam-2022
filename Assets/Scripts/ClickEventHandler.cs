using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnMousePointerDown;
    public UnityEvent OnMousePointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnMousePointerDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnMousePointerUp.Invoke();
    }
}
