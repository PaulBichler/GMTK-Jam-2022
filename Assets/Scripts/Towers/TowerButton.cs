using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TowerScriptableObject tower;

    private PlaceableUnit _spawnedTower;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        
        if(tower) _image.sprite = tower.iconSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    { 
        _spawnedTower = PlayerManager.Instance.SpawnTower(tower);
        _spawnedTower.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _spawnedTower.OnPointerUp(eventData);
    }
}