public class NormalTower : AttackTowerBase
{
    protected override void Attack(Enemy target)
    {
        var newBullet = ProjectileFactory.Instance.CreateProjectile(Data.projectileData);
        newBullet.transform.position = transform.position;
        newBullet.Initialize(Data.projectileData);
        newBullet.LaunchBullet(transform, target.transform, new DamageInfo(Data.damage));
        base.Attack(target);
    }
}
