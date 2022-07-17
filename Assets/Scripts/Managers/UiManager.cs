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
    public Button nextButton;
    public Canvas nextButtonCanvas;
    [SerializeField] private Shop shop;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject towerSelectionPanel;
    [SerializeField] private List<TowerButton> towerButtons;
    [SerializeField] private TowerDiceHand towerDiceHand;
    [SerializeField] private GameObject towerDiceHandPanel;


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

        //if(onNextClick == null)
        EnableNextButton("Proceed To Customization", onNextClick);
    }

    public void TransitionToDiceCustomization(UnityAction onNextClick)
    {
        HideShop();
        DisableNextButton();

        towerDiceHandPanel.SetActive(true);
        shop.StartShop();

        EnableNextButton("Proceed To Rolling", onNextClick);
    }
    
    public void TransitionToDiceRolling(UnityAction onNextClick)
    {
        towerDiceHand.ProceedPhase();
        DisableNextButton();

        EnableNextButton("Start Round", onNextClick);
    }

    public void HideRolling()
    {
        backgroundPanel.SetActive(false);
        towerDiceHand.ProceedPhase();
        towerDiceHandPanel.SetActive(false);
    }

    public void HideCustomization()
    {
        towerDiceHand.ProceedPhase();
    }

    public void HideShop()
    {
        //backgroundPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void HideDiceCustomization()
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
        nextButtonCanvas.enabled = true;
        nextButton.gameObject.SetActive(true);
        nextButton.GetComponentInChildren<TextMeshProUGUI>().SetText(buttonText);
        nextButton.onClick.AddListener(onNextClick);
    }

    public void DisableNextButton()
    {
        nextButtonCanvas.enabled = false;
        nextButton.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        nextButton.onClick.RemoveAllListeners();
        nextButton.gameObject.SetActive(false);
    }

    public void Announce(string message, float duration, UnityAction onEnd)
    {
        announcer.Announce(message, duration, onEnd);
    }

    public void HideTowerSelection()
    {
        towerSelectionPanel.SetActive(false);
    }

    public void ShowTowerSelection()
    {
        towerSelectionPanel.SetActive(true);
    }
}
