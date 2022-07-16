using UnityEngine;

public class NormalTower : AttackTowerBase
{
    protected override void Attack(Enemy target)
    {
        var newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        newBullet.Initialize(Data.projectileData);
        newBullet.LaunchBullet(transform, target.transform, new DamageInfo(Data.damage));
        base.Attack(target);
    }
}
