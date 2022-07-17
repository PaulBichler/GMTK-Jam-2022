using UnityEngine;

public class PiercingProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ReceiveDamage(DamageInfo);
        }
    }
}
