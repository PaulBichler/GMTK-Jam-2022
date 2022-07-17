using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Shop : MonoBehaviour
{
    public List<TowerScriptableObject> towerList = new List<TowerScriptableObject>();

    public List<TowerScriptableObject> selectedTowerList = new List<TowerScriptableObject>();
    public List<TowerDisplay> towerDisplayList = new List<TowerDisplay>();

    public List<TowerScriptableObject> boughtTowerScriptableObjectList = new List<TowerScriptableObject>();

    public Currency currency;

    void PopulateList()
    {
        string[] assetNames = AssetDatabase.FindAssets("Tower", new[] { "Assets/Data/Towers" });
        towerList.Clear();
        foreach (string SOName in assetNames)
        {
            string SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            TowerScriptableObject tower = AssetDatabase.LoadAssetAtPath<TowerScriptableObject>(SOpath);

            for (int i = 0; i < tower.shopWeight; i++) towerList.Add(tower);
        }
    }

    void GetShopOptions()
    {
        towerDisplayList.Clear();
        selectedTowerList.Clear();
        TowerDisplay[] towerDisplayChildren = transform.root.GetComponentsInChildren<TowerDisplay>();
        foreach (var child in towerDisplayChildren)
        {
            int randomIndex = Random.Range(0, towerList.Count);
            towerDisplayList.Add(child);
            selectedTowerList.Add(towerList[randomIndex]);
            towerDisplayList.Last<TowerDisplay>().tower = towerList[randomIndex];
        }
    }

    public void ToggleShop(bool toggle)
    {
        foreach(var towerDisplay in towerDisplayList)
        {
            towerDisplay.canvas.enabled = toggle;
            towerDisplay.emptyShopTile.SetActive(toggle);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        PopulateList();
        currency.ResetCurrency();
        GetShopOptions();
        //ToggleShop(false);
    }

    public void StartShop()
    {
        boughtTowerScriptableObjectList.Clear();
        currency.ResetCurrency();
        GetShopOptions();
    }

    public void CanRerollShop()
    {
        if (currency.CanRerollShop())
        {
            ToggleShop(true);
            GetShopOptions();
        }
    }

    private void OnEnable()
    {
        GetShopOptions();
    }

    public List<TowerScriptableObject> SendBoughtTowersForCustomization()
    {
        return boughtTowerScriptableObjectList;
    }

    public int SendCurrencyForCustomization()
    {
        return currency.currency;
    }
}
