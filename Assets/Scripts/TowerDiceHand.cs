using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDiceHand : MonoBehaviour
{
    public List<TowerScriptableObject> helperList = new List<TowerScriptableObject>();

    public TowerDiceDisplay[] TowerDiceDisplayArray;

    public List<Dice> allDiceList = new List<Dice>();

    public Dice SelectedDice;

    public List<TowerScriptableObject> SelectedTowersForBattle = new List<TowerScriptableObject>();

    public Canvas CloseSelectedDiceButton;
    public Canvas RollDiceButton;
    public Canvas ProceedButton;
    public Canvas InformationCanvas;

    public bool rollPhase = false;

    public Currency currency;

    public void CheckForSwap()
    {
        int backupTowerIndex = -1;
        TowerScriptableObject backupTowerScriptableObject = null;

        int backupDiceTowerIndex = -1;
        TowerScriptableObject backupDiceTowerScriptableObject = null;
        for (int i = 0; i < TowerDiceDisplayArray.Length; i++)
        {
            if (TowerDiceDisplayArray[i].isSelectedForSwap && backupTowerIndex == -1)
            {
                backupTowerIndex = i;
                backupTowerScriptableObject = TowerDiceDisplayArray[i].tower; 
            }
            else if (TowerDiceDisplayArray[i].isSelectedForSwap && backupTowerIndex != -1)
            {
                TowerDiceDisplayArray[backupTowerIndex].tower = TowerDiceDisplayArray[i].tower;
                TowerDiceDisplayArray[i].tower = backupTowerScriptableObject;

                TowerDiceDisplayArray[backupTowerIndex].isSelectedForSwap = false;
                TowerDiceDisplayArray[backupTowerIndex].outlineSpriteRenderer.color = Color.white;
                TowerDiceDisplayArray[backupTowerIndex].transform.localScale = Vector3.one;
                TowerDiceDisplayArray[i].isSelectedForSwap = false;
                TowerDiceDisplayArray[i].outlineSpriteRenderer.color = Color.white;
                TowerDiceDisplayArray[i].transform.localScale = Vector3.one;

                backupTowerIndex = -1;
                backupTowerScriptableObject = null;
            }
        }
        if(SelectedDice != null)
        {
            for (int i = 0; i < SelectedDice.TowerDiceDisplayList.Count; i++)
            {
                if (SelectedDice.TowerDiceDisplayList[i].isSelectedForSwap && backupDiceTowerIndex == -1)
                {
                    backupDiceTowerIndex = i;
                    backupDiceTowerScriptableObject = SelectedDice.TowerDiceDisplayList[i].tower;
                }
                else if (SelectedDice.TowerDiceDisplayList[i].isSelectedForSwap && backupDiceTowerIndex != -1)
                {
                    SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].tower = SelectedDice.TowerDiceDisplayList[i].tower;
                    SelectedDice.TowerDiceDisplayList[i].tower = backupDiceTowerScriptableObject;

                    SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].isSelectedForSwap = false;
                    SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].outlineSpriteRenderer.color = Color.white;
                    SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].transform.localScale = Vector3.one;
                    SelectedDice.TowerDiceDisplayList[i].isSelectedForSwap = false;
                    SelectedDice.TowerDiceDisplayList[i].outlineSpriteRenderer.color = Color.white;
                    SelectedDice.TowerDiceDisplayList[i].transform.localScale = Vector3.one;

                    backupDiceTowerIndex = -1;
                    backupDiceTowerScriptableObject = null;
                }
            }
            if(backupTowerIndex != -1 && backupDiceTowerIndex != -1)
            {
                TowerDiceDisplayArray[backupTowerIndex].tower = SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].tower;
                SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].tower = backupTowerScriptableObject;

                TowerDiceDisplayArray[backupTowerIndex].isSelectedForSwap = false;
                TowerDiceDisplayArray[backupTowerIndex].outlineSpriteRenderer.color = Color.white;
                TowerDiceDisplayArray[backupTowerIndex].transform.localScale = Vector3.one;
                SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].isSelectedForSwap = false;
                SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].outlineSpriteRenderer.color = Color.white;
                SelectedDice.TowerDiceDisplayList[backupDiceTowerIndex].transform.localScale = Vector3.one;
            }
        }
    }

    public void CloseSelectedDice()
    {
        if(SelectedDice)
        {
            SelectedDice.CloseDice(); 
        }
    }

    public void HideOtherDice()
    {
        foreach(Dice child in allDiceList)
        {
            if (SelectedDice == child)
            {
                child.ShowDice();
            }
            else
            {
                child.HideDice();
            }
        }
    }

    public void AddTowerForBattle(Dice dice)
    {
        SelectedTowersForBattle.Add(dice.selectedTowerScriptableObject);
        dice.selectedTowerScriptableObject = null;
        dice.HideSelectedTower();
    }

    public void ProceedPhase()
    {
        if (!rollPhase)
        {
            SelectedTowersForBattle.Clear();
            rollPhase = true;
            RollDiceButton.enabled = true;
            InformationCanvas.enabled = false;
            currency.ShowCurrency();
            foreach (Dice child in allDiceList)
            {
                child.ShowSelectedTower();
                child.SelectRandomTowerFromList();
            }
            foreach (TowerDiceDisplay child in TowerDiceDisplayArray)
            {
                child.canvas.enabled = false;
                child.emptyShopTile.SetActive(false);
            }
        }
        //here you go ahead and change to tower placement phase and reset rollphase to false
        else
        {
            rollPhase = false;
            RollDiceButton.enabled = false;
            InformationCanvas.enabled = true;
            currency.HideCurrency();
            foreach (Dice child in allDiceList)
            {
                child.HideSelectedTower();
            }
            foreach (TowerDiceDisplay child in TowerDiceDisplayArray)
            {
                child.canvas.enabled = true;
                child.emptyShopTile.SetActive(true);
            }
        }
    }

    public void ShowOtherDice()
    {
        foreach (Dice child in allDiceList)
        {
            child.ShowDice();
        }
    }

    void GetHandFromShop(List<TowerScriptableObject> towerScriptableObjects)
    {
        if (towerScriptableObjects.Count != 0)
        {
            if (towerScriptableObjects.Count == 1)
            {
                TowerDiceDisplayArray[0].tower = null;
                TowerDiceDisplayArray[1].tower = towerScriptableObjects[0];
                TowerDiceDisplayArray[2].tower = null;
            }
            else if (towerScriptableObjects.Count == 2)
            {
                TowerDiceDisplayArray[0].tower = towerScriptableObjects[0];
                TowerDiceDisplayArray[1].tower = null;
                TowerDiceDisplayArray[2].tower = towerScriptableObjects[1];
            }
            else if (towerScriptableObjects.Count == 3)
            {
                TowerDiceDisplayArray[0].tower = towerScriptableObjects[0];
                TowerDiceDisplayArray[1].tower = towerScriptableObjects[1];
                TowerDiceDisplayArray[2].tower = towerScriptableObjects[2];
            }
        } 
        else
        {
            TowerDiceDisplayArray[0].tower = null;
            TowerDiceDisplayArray[1].tower = null;
            TowerDiceDisplayArray[2].tower = null;
        }
    }

    public void CanRerollDice()
    {
        if (currency.CanRerollDice())
        {
            foreach (Dice child in allDiceList)
            {
                if(child.selectedTowerScriptableObject != null)
                {
                    child.SelectRandomTowerFromList();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetHandFromShop(helperList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
