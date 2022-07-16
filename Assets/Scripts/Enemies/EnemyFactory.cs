using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [Serializable]
    private struct EnemyDictionaryData
    {
        public EnemyData dataKey;
        public GameObject prefabValue;
    }

    [SerializeField] private List<EnemyDictionaryData> enemyPrefabDictionary;

    private Dictionary<EnemyData, GameObject> _enemyPrefabs;

    public static EnemyFactory Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;

        _enemyPrefabs = new Dictionary<EnemyData, GameObject>();

        foreach (var dictionaryData in enemyPrefabDictionary)
            _enemyPrefabs.Add(dictionaryData.dataKey, dictionaryData.prefabValue);

    }

    public Enemy CreateEnemy(EnemyData data, List<Transform> pathToTravel)
    {
        if (!_enemyPrefabs.ContainsKey(data))
        {
            print($"Enemy Prefab of {data.name} does not exist in dictionary!");
            return null;
        }
        
        var enemy = Instantiate(_enemyPrefabs[data]).GetComponent<Enemy>();
        enemy.Initialize(data, pathToTravel);
        return enemy;
    }
}
