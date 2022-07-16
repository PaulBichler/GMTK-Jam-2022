using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour, IPathTraveler
{
    [SerializeField] private float movementSpeed;

    private List<Transform> _path;
    private int _currentIndex;
    private Transform _currentDestination;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
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
        _rb2d.velocity = dir * movementSpeed;

        if (Vector2.Distance(position, destination) < 0.1)
        {
            GoToNext(_currentIndex + 1);
        }
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
}
