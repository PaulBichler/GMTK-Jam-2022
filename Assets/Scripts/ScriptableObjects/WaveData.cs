using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Scriptable Objects/Waves/Wave Data")]
public class WaveData : ScriptableObject
{
    [SerializeField] private List<EnemyWithDelay> enemiesToSpawn;

    public ReadOnlyCollection<EnemyWithDelay> EnemiesToSpawn => enemiesToSpawn.AsReadOnly();
}

[Serializable]
public struct EnemyWithDelay
{
    public EnemyData enemyData;
    public float waitAfterSpawn;
}
