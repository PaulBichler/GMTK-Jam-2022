using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnHoverEnter;
    public UnityEvent OnHoverExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit.Invoke();
    }
}
