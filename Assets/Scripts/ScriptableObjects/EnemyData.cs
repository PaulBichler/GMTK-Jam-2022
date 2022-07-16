using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public Sprite sprite;
    public float movementSpeed;
    public float health;
}
