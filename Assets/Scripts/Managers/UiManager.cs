using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    
    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private UiAnnouncer announcer;
    [SerializeField] private Button nextButton;
    [SerializeField] private Shop shop;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private List<TowerButton> towerButtons;

    private void Awake()
    {
        if(Instance)
            Destroy(this);
        else
            Instance = this;
        
        DisableNextButton();
    }

    public void TransitionToShop(UnityAction onNextClick)
    {
        ClearTowerSelections();
        backgroundPanel.SetActive(true);
        shopPanel.SetActive(true);
        shop.ToggleShop(true);
        shop.StartShop();

        EnableNextButton("Next Wave", onNextClick);
    }

    public void HideShop()
    {
        backgroundPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void ClearTowerSelections()
    {
        foreach (var towerButton in towerButtons)
            towerButton.SetTower(null);
    }

    public void AddTowerToSelection(TowerScriptableObject towerData)
    {
        foreach (var towerButton in towerButtons)
        {
            if (!towerButton.HasTower())
            {
                towerButton.SetTower(towerData);
                return;
            }
        }
    }

    public void EnableNextButton(string buttonText, UnityAction onNextClick)
    {
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);
        nextButton.onClick.AddListener(onNextClick);
    }

    public void DisableNextButton()
    {
        nextButton.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        nextButton.onClick.RemoveAllListeners();
        nextButton.gameObject.SetActive(false);
    }

    public void Announce(string message, float duration, UnityAction onEnd)
    {
        announcer.Announce(message, duration, onEnd);
    }
}
