using UnityEngine;

[CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Objects/Towers/Tower Data")]
public class TowerData : ScriptableObject
{
    public TowerType type;
    public Sprite sprite;
    public float damage;
    public float range;
    public float attackSpeed;
    public ProjectileData projectileData;
}

public enum TowerType
{
    Normal
}
