using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour, IPointerDownHandler
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
        Color color = _image.color;
        
        if (tower)
        {
            _image.sprite = tower.iconSprite;
            color.a = 1;
        }
        else
        {
            _image.sprite = null;
            color.a = 0;
        }
        
        _image.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!tower) return;
        
        _spawnedTower = PlayerManager.Instance.SpawnTower(tower);
        _spawnedTower.OnPointerDown(eventData);
        //_spawnedTower.OnPlaced.AddListener(() => SetTower(null));
        SetTower(null);
    }

    public void SetTower(TowerScriptableObject towerData)
    {
        tower = towerData;
        UpdateIcon();
    }

    public bool HasTower() => tower != null;
}
