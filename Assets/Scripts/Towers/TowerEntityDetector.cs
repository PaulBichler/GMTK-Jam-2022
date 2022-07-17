using UnityEngine;

public class TowerEntityDetector : MonoBehaviour
{
    [SerializeField] private Transform radiusCircle;

    private CircleCollider2D _col;
    private TowerBase _owner;

    private void Awake()
    {
        _col = GetComponent<CircleCollider2D>();
    }

    public void Initialize(TowerBase owner, float range)
    {
        _owner = owner;
        SetRange(range);
    }

    public void SetRange(float range)
    {
        _col.radius = range;
        radiusCircle.localScale = new Vector3(range * 2, range * 2, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_owner)
            _owner.OnEntityEnterRange(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(_owner)
            _owner.OnEntityExitRange(other.gameObject);
    }
}
