using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    
    [SerializeField] private GameObject unitPanel;
    [SerializeField] private List<Button> towerButtons;

    private void Awake()
    {
        if(Instance)
            Destroy(this);
        else
            Instance = this;
        
    }
    
}
