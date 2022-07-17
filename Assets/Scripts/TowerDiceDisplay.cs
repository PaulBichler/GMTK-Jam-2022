using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerDiceDisplay : MonoBehaviour
{
    public TowerScriptableObject tower;

    public TextMeshProUGUI nameText, descriptionText, damageText, attackRangeText, attackSpeedText;
    public Image iconImage, backgroundImage;

    public Canvas canvas;
    public GameObject emptyShopTile;
    public SpriteRenderer outlineSpriteRenderer;

    public Shop shop;
    public Currency currency;

    public Dice dice;
    public TowerDiceHand towerDiceHand;
    public bool isSelectedForSwap;

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
        if(tower == null)
        {
            canvas.enabled = false;
            emptyShopTile.SetActive(false);
        }
        else
        {
            nameText.text = tower.name;
            descriptionText.text = tower.description;
            damageText.text = "Damage\n" + tower.damage.ToString();
            attackRangeText.text = "Attack Range\n" + tower.attackRange.ToString();
            attackSpeedText.text = "Attack Speed\n" + tower.attackSpeed.ToString();
            iconImage.sprite = tower.iconSprite;
        }
        
    }

    public void ToggleSelectedForSwap()
    {
        if(emptyShopTile != null)
        {
            if (isSelectedForSwap)
            {
                outlineSpriteRenderer.color = Color.white;
                transform.localScale = Vector3.one;
            }
            else
            {
                outlineSpriteRenderer.color = Color.green;
                transform.localScale = new Vector3(1.1f, 1.1f, 1);
            }
            isSelectedForSwap = !isSelectedForSwap;
            if (isSelectedForSwap) towerDiceHand.CheckForSwap();
        }
    }

    public void CloseDice()
    {
        isSelectedForSwap = false;
        outlineSpriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
        canvas.enabled = false;
        emptyShopTile.SetActive(false);
    }

    public void OpenDice()
    {
        canvas.enabled = true;
        emptyShopTile.SetActive(true);
    }


        public void ToggleTowerOverlay(bool isHovered)
    {
        nameText.enabled = isHovered;
        descriptionText.enabled = isHovered;
        damageText.enabled = isHovered;
        attackRangeText.enabled = isHovered;
        attackSpeedText.enabled = isHovered;
        backgroundImage.enabled = isHovered;

        canvas.sortingOrder = isHovered ? 1 : 0;
    }

    public void BuyTower()
    {
        if(currency.CanBuyTower())
        canvas.enabled = false;
    }

}
