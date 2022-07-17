using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlaceableUnit : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isPlacing;
    private bool _isPlaced;
    private bool _isValidPlacement;

    public UnityEvent OnPlaced { get; } = new();

    protected virtual void Update()
    {
        if (_isPlacing)
        {
            transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
            _isValidPlacement = CheckPlacement();
        }
    }

    protected virtual bool CheckPlacement()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1.3f);
        
        if (cols.Length > 0)
        {
            foreach (var col in cols)
            {
                if (col.gameObject != gameObject && (col.gameObject.CompareTag("Tower") || col.gameObject.CompareTag("Path")))
                    return false;
            }
        }

        return true;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (_isPlaced) return;
        
        if (_isPlacing && _isValidPlacement)
        {
            OnPlace();
            return;
        }

        _isPlacing = true;

        if (UiManager.Instance)
            UiManager.Instance.HideTowerSelection();
    }

    public virtual void OnPlace()
    {
        _isPlaced = true;
        _isPlacing = false;
        OnPlaced?.Invoke();
        
        if (UiManager.Instance)
            UiManager.Instance.ShowTowerSelection();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }
}
