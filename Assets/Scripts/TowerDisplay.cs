using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TowerDisplay : MonoBehaviour
{
    public TowerScriptableObject tower;

    public TextMeshProUGUI nameText, descriptionText, damageText, attackRangeText, attackSpeedText;
    public Image iconImage, backgroundImage;

    public Canvas canvas;
    public GameObject emptyShopTile;

    public Shop shop;
    public Currency currency;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTower();
        ToggleTowerOverlay(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTower();
    }

    public void UpdateTower()
    {
        nameText.text = tower.name;
        descriptionText.text = tower.description;
        damageText.text = "Damage\n" + tower.damage.ToString();
        attackRangeText.text = "Attack Range\n" + tower.attackRange.ToString();
        attackSpeedText.text = "Attack Speed\n" + tower.attackSpeed.ToString();
        iconImage.sprite = tower.iconSprite;
    }

    public void ToggleTowerOverlay(bool isHovered)
    {
        nameText.enabled = isHovered;
        descriptionText.enabled = isHovered;
        damageText.enabled = isHovered;
        attackRangeText.enabled = isHovered;
        attackSpeedText.enabled = isHovered;
        backgroundImage.enabled = isHovered;

        canvas.sortingOrder = isHovered ? 3 : 2;
    }

    public void BuyTower()
    {
        if(currency.CanBuyTower())
        {
            canvas.enabled = false;
            shop.boughtTowerScriptableObjectList.Add(tower);
        }
            
        
        //if(UiManager.Instance)
        //    UiManager.Instance.AddTowerToSelection(tower);
    }

}
