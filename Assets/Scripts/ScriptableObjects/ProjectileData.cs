using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Data", menuName = "Scriptable Objects/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public Sprite sprite;
    public float speed;
    public float aoeRange = -1;
    public float aoeDamage = -1;
}
