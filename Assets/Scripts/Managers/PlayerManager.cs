using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int baseHealth;
    [SerializeField] private List<GameObject> availableTowers;

    private Camera _camera;
    
    public static PlayerManager Instance { get; private set; }

    public List<GameObject> AvailableTowers => availableTowers;

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
        
        _camera = Camera.main;
    }

    public void BaseCollision()
    {
        baseHealth--;
    }

    public PlaceableUnit SpawnTower(TowerData data)
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        var newTower = TowerFactory.Instance.CreateTower(data);
        newTower.transform.position = mousePos;
        newTower.transform.rotation = Quaternion.identity;
        return newTower;
    }
}
