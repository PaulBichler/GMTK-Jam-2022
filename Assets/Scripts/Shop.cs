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

    void ToggleShop(bool toggle)
    {
        foreach(var towerDisplay in towerDisplayList)
        {
            towerDisplay.canvas.enabled = toggle;
            towerDisplay.emptyShopTile.SetActive(toggle);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
        currency.ResetCurrency();
        GetShopOptions();
        //ToggleShop(false);
    }

    public void StartShop()
    {
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



    // Update is called once per frame
    void Update()
    {
        
    }
}
