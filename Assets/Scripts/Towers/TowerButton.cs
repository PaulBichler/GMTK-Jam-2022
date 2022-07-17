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
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        _image.sprite = tower ? tower.iconSprite : null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!tower) return;
        
        _spawnedTower = PlayerManager.Instance.SpawnTower(tower);
        _spawnedTower.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_spawnedTower) return;
        _spawnedTower.OnPointerUp(eventData);
    }

    public void SetTower(TowerScriptableObject towerData)
    {
        tower = towerData;
        UpdateIcon();
    }

    public bool HasTower() => tower != null;
}
