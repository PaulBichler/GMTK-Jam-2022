using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveData> waves;
    [SerializeField] private Transform pathParent;

    private readonly List<Transform> _path = new();
    private int _currentWaveIndex = -1;
    private List<Enemy> _activeEnemies;
    private int _enemyDeathCount;

    public static WaveManager Instance { get; private set; }
    public int CurrentWave => _currentWaveIndex + 1;
    public UnityEvent OnWaveEnd { get; } = new();

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
        
        _activeEnemies = new List<Enemy>();
        foreach (Transform child in pathParent)
            _path.Add(child.transform);
    }

    public void StartNextWave(UnityAction onWaveEnd)
    {
        if(_currentWaveIndex == waves.Count - 1)
            return;
        
        if(onWaveEnd != null)
            OnWaveEnd.AddListener(onWaveEnd);
        
        StartCoroutine(StartWaveCoroutine(waves[++_currentWaveIndex]));
    }

    private IEnumerator StartWaveCoroutine(WaveData wave)
    {
        foreach (var enemy in wave.EnemiesToSpawn)
        {
            SpawnEnemy(enemy.enemyData);
            yield return new WaitForSeconds(enemy.waitAfterSpawn);
        }
    }

    private void SpawnEnemy(EnemyData data)
    {
        var enemy = EnemyFactory.Instance.CreateEnemy(data, _path);
        enemy.transform.rotation = Quaternion.identity;
        enemy.transform.position = _path[0].position;
        enemy.Initialize(data, _path);
        enemy.onDeath.AddListener(EnemyDied);
        _activeEnemies.Add(enemy);
    }

    private void EnemyDied(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
        enemy.onDeath.RemoveListener(EnemyDied);
        _enemyDeathCount++;

        if (_activeEnemies.Count == 0 && _enemyDeathCount == waves[_currentWaveIndex].EnemiesToSpawn.Count)
            OnWaveEnd?.Invoke();
    }
}
