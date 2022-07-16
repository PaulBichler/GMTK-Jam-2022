using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private List<Transform> path = new List<Transform>();
    [SerializeField] private WaveData waveData;
    [SerializeField] private Transform pathParent;


    private List<Enemy> _activeEnemies;

    private void Awake()
    {
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
