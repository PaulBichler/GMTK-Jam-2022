using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private int baseHealth;
    [SerializeField] private List<Action> actions;

    public void BaseCollision()
    {
        baseHealth--;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //TODO do raycast to get unit
        print(eventData.pointerClick);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //TODO place unit if you're holding one
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //TODO move unit with mouse
    }
}
