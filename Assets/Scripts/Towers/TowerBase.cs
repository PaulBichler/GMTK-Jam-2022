using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TowerState
{
    Idle,
    Cooldown,
}

public abstract class TowerBase : PlaceableUnit
{
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected TowerScriptableObject Data;
    protected TowerEntityDetector EntityDetector;
    protected bool IsInitialized;

    protected TowerState State;
    protected List<GameObject> EntitiesInRange;
    
    private WaitForSeconds _cooldownTimer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EntityDetector = GetComponentInChildren<TowerEntityDetector>();
        EntitiesInRange = new List<GameObject>();
    }

    public virtual void InitializeTower(TowerScriptableObject data)
    {
        if (IsInitialized) return;
        
        Data = Instantiate(data);

        if (Data.getRandomStats && Data.statRangeData)
        {
            var statRanges = Data.statRangeData;
            Data.damage = Random.Range(statRanges.minDamage, statRanges.maxDamage);
            Data.attackSpeed = Random.Range(statRanges.minAttackSpeed, statRanges.maxAttackSpeed);
            Data.attackRange = Random.Range(statRanges.minAttackRange, statRanges.maxAttackRange);
        }
        
        spriteRenderer.sprite = Data.iconSprite;
        EntityDetector.Initialize(this, Data.attackRange);
        IsInitialized = true;
    }
    
    public virtual void OnEntityEnterRange(GameObject entity)
    {
        EntitiesInRange.Add(entity);
    }
    
    public virtual void OnEntityExitRange(GameObject entity)
    {
        EntitiesInRange.Remove(entity);
    }

    protected virtual IEnumerator StartCooldownTimer(float time, System.Action onTimerEnd)
    {
        State = TowerState.Cooldown;
        yield return new WaitForSeconds(time);
        State = TowerState.Idle;
        onTimerEnd?.Invoke();
    }
}
