using UnityEngine;

[CreateAssetMenu(fileName = "Stat Range Data", menuName = "Scriptable Objects/State Range Data")]
public class StatRangeData : ScriptableObject
{
    public float minDamage;
    public float maxDamage;
    public float minAttackSpeed;
    public float maxAttackSpeed;
    public float minAttackRange;
    public float maxAttackRange;
}
