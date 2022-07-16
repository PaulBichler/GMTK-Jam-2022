using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private List<Transform> path = new List<Transform>();
    [SerializeField] private WaveData waveData;
    [SerializeField] private Transform pathParent;


    private List<Enemy> _activeEnemies;
    
    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
        
        _activeEnemies = new List<Enemy>();
        foreach (Transform child in pathParent) {
            //Debug.Log(child.transform);
            path.Add(child.transform);
        }
        StartCoroutine(StartWave(waveData));
        Debug.Log(path[0]);
    }

    private IEnumerator StartWave(WaveData wave)
    {
        foreach (var enemy in wave.EnemiesToSpawn)
        {
            SpawnEnemy(enemy.enemyData);
            yield return new WaitForSeconds(enemy.waitAfterSpawn);
        }
    }

    private void SpawnEnemy(EnemyData data)
    {
        var enemy = EnemyFactory.Instance.CreateEnemy(data, path);
        enemy.transform.rotation = Quaternion.identity;
        enemy.transform.position = path[0].position;
        _activeEnemies.Add(enemy);
        enemy.Initialize(data, path);
    }
}
