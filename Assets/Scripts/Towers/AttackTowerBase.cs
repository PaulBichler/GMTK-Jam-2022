using System.Collections.Generic;
using UnityEngine;

public class AttackTowerBase : TowerBase
{
    [SerializeField] protected GameObject bulletPrefab;
    
    protected List<Enemy> EnemiesInRange;

    protected override void Awake()
    {
        EnemiesInRange = new List<Enemy>();
        base.Awake();
    }

    public override void OnEntityEnterRange(GameObject entity)
    {
        if (!entity.CompareTag("Enemy")) return;
        
        var enemy = entity.GetComponent<Enemy>();
        EnemiesInRange.Add(enemy);
        enemy.onDeath.AddListener(OnEnemyDeath); 
        TryAttack();
    }

    public override void OnEntityExitRange(GameObject entity)
    {
        if (!entity.CompareTag("Enemy")) return;
        
        var enemy = entity.GetComponent<Enemy>();
        enemy.onDeath.RemoveListener(OnEnemyDeath);
        EnemiesInRange.Remove(enemy);
        TryAttack();
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        EnemiesInRange.Remove(enemy);
    }

    protected virtual void TryAttack()
    {
        if(State == TowerState.Idle && EnemiesInRange.Count > 0)
            Attack(GetFirstEnemy(EnemiesInRange));
    }

    protected virtual void Attack(Enemy target)
    {
        StartCoroutine(StartCooldownTimer(1 / Data.attackSpeed, TryAttack));
    }

    protected virtual Enemy GetFirstEnemy(List<Enemy> enemiesInRange)
    {
        enemiesInRange.Sort((e1, e2) => e2.CurrentIndex.CompareTo(e1.CurrentIndex));
        return enemiesInRange[0];
    }
}