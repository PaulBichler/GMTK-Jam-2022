using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D Rb2d;
    protected DamageInfo DamageInfo;
    protected ProjectileData Data;

    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize(ProjectileData data)
    {
        Data = Instantiate(data);
    }

    public virtual void LaunchBullet(Transform owner, Transform target, DamageInfo damageInfo)
    {
        Vector2 dir = (target.position - owner.position).normalized;
        Rb2d.velocity = dir * Data.speed;
        DamageInfo = damageInfo;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ReceiveDamage(DamageInfo);
            
            if(Data.aoeRange > 0)
                DoAoeDamage(transform.position);
            
            Destroy(gameObject);
        }
    }

    protected virtual void DoAoeDamage(Vector2 impactPosition)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(impactPosition, Data.aoeRange);

        if (colls.Length > 0)
        {
            foreach (var coll in colls)
            {
                if(coll.TryGetComponent<Enemy>(out var enemy))
                    enemy.ReceiveDamage(new DamageInfo(DamageInfo.DamageToApply * Data.aoeDamageMultiplier));
            }
        }
    }
}