using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputInteractable
{
    void OnDown(InputInfo info);
    void OnUp(InputInfo info);
    void OnDrag(InputInfo info);
}

public class InputInfo
{
    
}