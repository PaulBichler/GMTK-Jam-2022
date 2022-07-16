using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IPathTraveler
{
    private List<Transform> _path;
    private int _currentIndex;
    private Transform _currentDestination;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2d;

    public UnityEvent<Enemy> onDeath;

    protected EnemyData Data;

    public int CurrentIndex => _currentIndex;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_currentDestination) 
            return;

        Transform myTransform = transform;
        Vector3 position = myTransform.position;
        Vector3 destination = _currentDestination.position;
        
        Vector2 dir = (destination - position).normalized;
        myTransform.up = dir;
        _rb2d.velocity = dir * Data.movementSpeed;

        if (Vector2.Distance(position, destination) < 0.1)
        {
            GoToNext(_currentIndex + 1);
        }
    }

    public void Initialize(EnemyData data, List<Transform> pathToTravel)
    {
        Data = Instantiate(data);
        _spriteRenderer.sprite = data.sprite;
        StartTravel(pathToTravel);
    }

    public void ReceiveDamage(DamageInfo damageInfo)
    {
        if(damageInfo == null)
            return;
        
        Data.health -= damageInfo.DamageToApply;

        if (Data.health <= 0)
            Die();
    }

    public void StartTravel(List<Transform> pathToTravel)
    {
        _path = pathToTravel;
        GoToNext(1);
    }

    public void GoToNext(int pathIndex)
    {
        _currentIndex = pathIndex;
        _currentDestination = _path[pathIndex];
    }

    private void Die()
    {
        onDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
