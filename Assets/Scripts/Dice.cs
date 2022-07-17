using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public List<TowerDiceDisplay> TowerDiceDisplayList = new List<TowerDiceDisplay>();

    public TowerDiceHand TowerDiceHand;

    public Canvas diceFaceCanvas;

    public bool isDiceOpen = false;

    public Vector3 startPos = Vector3.zero;

    public TowerScriptableObject selectedTowerScriptableObject = null;

    public TowerDiceDisplay selectedTower;

    public void CloseDice()
    {
        if (isDiceOpen)
        {
            foreach (var towerDisplay in TowerDiceDisplayList)
            {
                towerDisplay.CloseDice();
            }
            isDiceOpen = false;
            TowerDiceHand.SelectedDice = null;

            transform.position = startPos;
            //TowerDiceHand.currency.ShowCurrency();
            //TowerDiceHand.RollDiceButton.SetActive(true);
            TowerDiceHand.currency.HideCurrency();
            TowerDiceHand.RollDiceButton.enabled = false;

            TowerDiceHand.InformationCanvas.enabled = true;
            TowerDiceHand.ProceedButton.enabled = true;
            TowerDiceHand.CloseSelectedDiceButton.enabled =false;
        }
    }

    public void OpenDice()
    {
        if(!TowerDiceHand.rollPhase)
        {
            TowerDiceHand.CloseSelectedDice();
            if (!isDiceOpen)
            {
                foreach (var towerDisplay in TowerDiceDisplayList)
                {
                    towerDisplay.OpenDice();
                }
                transform.position = Vector3.zero;
                TowerDiceHand.SelectedDice = this;
                isDiceOpen = true;
                TowerDiceHand.currency.HideCurrency();
                TowerDiceHand.RollDiceButton.enabled = false;
                TowerDiceHand.ProceedButton.enabled = false;
                TowerDiceHand.InformationCanvas.enabled = false;
                TowerDiceHand.CloseSelectedDiceButton.enabled = true;
            }
            TowerDiceHand.HideOtherDice();
        }
    }

    public void AddTowerForBattle()
    {
        TowerDiceHand.AddTowerForBattle(this);
    }

    public void SelectRandomTowerFromList()
    {
        int randomNumber = Random.Range(0, TowerDiceDisplayList.Count);
        selectedTowerScriptableObject = TowerDiceDisplayList[randomNumber].tower;
        selectedTower.tower = selectedTowerScriptableObject;
    }

    public void HideDice()
    {
        diceFaceCanvas.enabled = false;
    }

    public void ShowDice()
    {
        diceFaceCanvas.enabled = true;
    }

    public void MoveToRollPos()
    {
        transform.position = new Vector3(startPos.x, 0, 0);
    }

    public void ResetToDiceSelectPhase()
    {
        transform.position = startPos;
    }

    public void HideSelectedTower()
    {
        selectedTower.canvas.enabled = false;
    }

    public void ShowSelectedTower()
    {
        selectedTower.canvas.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        isDiceOpen = true;
        HideSelectedTower();
        CloseDice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
