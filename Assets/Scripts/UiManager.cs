using System;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    
    [SerializeField] private GameObject unitPanel;

    private void Awake()
    {
        if(Instance)
            Destroy(this);
        else
            Instance = this;
        
        
    }
}
