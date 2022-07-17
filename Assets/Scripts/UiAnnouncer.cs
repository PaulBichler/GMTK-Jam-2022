using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UiAnnouncer : MonoBehaviour
{
    [SerializeField] private GameObject announcerPanel;
    [SerializeField] private TextMeshProUGUI messageDisplay;

    public void Announce(string text, float duration, UnityAction onEnd)
    {
        StartCoroutine(AnnounceCoroutine(text, duration, onEnd));
    }

    private IEnumerator AnnounceCoroutine(string text, float duration, UnityAction onEnd)
    {
        messageDisplay.SetText(text);
        announcerPanel.SetActive(true);

        yield return new WaitForSeconds(duration);
        
        messageDisplay.SetText("");
        announcerPanel.SetActive(false);
        onEnd?.Invoke();
    }
}
