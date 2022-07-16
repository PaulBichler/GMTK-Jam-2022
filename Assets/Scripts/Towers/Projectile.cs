using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D _rb2d;
    private DamageInfo _damageInfo;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize(ProjectileData data)
    {
        speed = data.speed;
    }

    public void LaunchBullet(Transform owner, Transform target, DamageInfo damageInfo)
    {
        Vector2 dir = (target.position - owner.position).normalized;
        _rb2d.velocity = dir * speed;
        _damageInfo = damageInfo;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ReceiveDamage(_damageInfo);
            Destroy(gameObject);
        }
    }
}