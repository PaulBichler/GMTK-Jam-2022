using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [Serializable]
    private struct TowerDictionaryData
    {
        public TowerScriptableObject dataKey;
        public GameObject prefabValue;
    }

    [SerializeField] private List<TowerDictionaryData> towerPrefabDictionary;

    private Dictionary<TowerScriptableObject, GameObject> _towerPrefabs;

    public static TowerFactory Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;

        _towerPrefabs = new Dictionary<TowerScriptableObject, GameObject>();

        foreach (var dictionaryData in towerPrefabDictionary)
            _towerPrefabs.Add(dictionaryData.dataKey, dictionaryData.prefabValue);

    }

    public TowerBase CreateTower(TowerScriptableObject data)
    {
        if (!_towerPrefabs.ContainsKey(data))
        {
            print($"Tower Prefab of {data.name} does not exist in dictionary!");
            return null;
        }
        
        var tower = Instantiate(_towerPrefabs[data]).GetComponent<TowerBase>();
        tower.InitializeTower(data);
        return tower;
        //return SetupTower(tower, data);
    }

    private TowerBase SetupTower(GameObject instance, TowerScriptableObject data)
    {
        switch (data.type)
        {
            case TowerType.Normal:
                return CreateNormalTower(instance, data);
        }

        return null;
    }

    private NormalTower CreateNormalTower(GameObject instance, TowerScriptableObject data)
    {
        var normalTower =  instance.GetComponent<NormalTower>();
        normalTower.InitializeTower(data);
        return normalTower;
    }
}
