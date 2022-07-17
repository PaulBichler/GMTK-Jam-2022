using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D Rb2d;
    protected DamageInfo DamageInfo;
    protected float Speed;

    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize(ProjectileData data)
    {
        Speed = data.speed;
    }

    public virtual void LaunchBullet(Transform owner, Transform target, DamageInfo damageInfo)
    {
        Vector2 dir = (target.position - owner.position).normalized;
        Rb2d.velocity = dir * Speed;
        DamageInfo = damageInfo;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ReceiveDamage(DamageInfo);
            Destroy(gameObject);
        }
    }
}