using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int currency = 10;

    public int startCurrency = 10;

    public int towerCost = 3;
    public int shopRerollCost = 1;
    public int diceRerollCost = 2;

    public TextMeshProUGUI currencyText;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HideCurrency()
    {
        canvas.enabled = false;
    }

    public void ShowCurrency()
    {
        canvas.enabled = true;
    }

    void UpdateCurrency() => currencyText.text = currency.ToString();

    public void ResetCurrency()
    {
        currency = startCurrency;
        UpdateCurrency();
    }

    public bool CanBuyTower()
    {
        if (currency >= towerCost)
        {
            BuyTower();
            return true;
        }
        else return false;
    }

    void BuyTower()
    {
        currency -= towerCost;
        UpdateCurrency();
    }

    public bool CanRerollShop()
    {
        if (currency >= shopRerollCost)
        {
            RerollShop();
            return true;
        }
        else return false;
    }

    void RerollShop()
    {
        currency -= shopRerollCost;
        UpdateCurrency();
    }

    public bool CanRerollDice()
    {
        if (currency >= diceRerollCost)
        {
            RerollDice();
            return true;
        }
        else return false;
    }

    void RerollDice()
    {
        currency -= diceRerollCost;
        UpdateCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
