using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Transform> path;
    [SerializeField] private WaveData waveData;

    private List<Enemy> _activeEnemies;

    private void Awake()
    {
        _activeEnemies = new List<Enemy>();
        StartCoroutine(StartWave(waveData));
    }

    private IEnumerator StartWave(WaveData wave)
    {
        foreach (var enemy in wave.EnemiesToSpawn)
        {
            SpawnEnemy(enemy.enemyPrefab);
            yield return new WaitForSeconds(enemy.waitAfterSpawn);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        var enemy = Instantiate(enemyPrefab, path[0].position, Quaternion.identity).GetComponent<Enemy>();
        _activeEnemies.Add(enemy);
        enemy.StartTravel(path);
    }
}
