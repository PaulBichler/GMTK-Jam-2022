using UnityEngine;

public class SupportTower : TowerBase
{
    public override void OnEntityEnterRange(GameObject entity)
    {
        if (!entity.CompareTag("Tower")) return;
        
        if (!Data.statRangeData)
        {
            print($"Influencer {Data.name} does not have a stat boost defined!");
            return;
        }
        
        if(entity.TryGetComponent<AttackTowerBase>(out var attackTower))
            attackTower.AddToStats(Data.statRangeData);
    }

    public override void OnEntityExitRange(GameObject entity)
    {
        if (!entity.CompareTag("Tower")) return;

        if (!Data.statRangeData)
        {
            print($"Influencer {Data.name} does not have a stat boost defined!");
            return;
        }
        
        if(entity.TryGetComponent<AttackTowerBase>(out var attackTower))
            attackTower.RemoveFromStats(Data.statRangeData);
    }
}
