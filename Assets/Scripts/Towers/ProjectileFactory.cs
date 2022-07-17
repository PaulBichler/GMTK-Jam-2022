using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [Serializable]
    private struct ProjectileDictionaryData
    {
        public ProjectileData dataKey;
        public GameObject prefabValue;
    }

    [SerializeField] private List<ProjectileDictionaryData> projectilePrefabDictionary;

    private Dictionary<ProjectileData, GameObject> _projectilePrefabs;

    public static ProjectileFactory Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;

        _projectilePrefabs = new Dictionary<ProjectileData, GameObject>();

        foreach (var dictionaryData in projectilePrefabDictionary)
            _projectilePrefabs.Add(dictionaryData.dataKey, dictionaryData.prefabValue);

    }

    public Projectile CreateProjectile(ProjectileData data)
    {
        if (!_projectilePrefabs.ContainsKey(data))
        {
            print($"Projectile Prefab of {data.name} does not exist in dictionary!");
            return null;
        }
        
        var projectile = Instantiate(_projectilePrefabs[data]).GetComponent<Projectile>();
        projectile.Initialize(data);
        return projectile;
    }
}
