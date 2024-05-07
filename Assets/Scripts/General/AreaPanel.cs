using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class AreaPanel : MonoBehaviour
{
    private RectTransform rectTransform;
    private IEventSystem eventSystem;
    private CanvasGrounpPlus canvas;
    private TextMeshProUGUI tmp;
    [HideInInspector]
    public string number;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGrounpPlus>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        eventSystem = ServiceLocator.Get<IEventSystem>();
    }

    private void OnEnable()
    {
        eventSystem.AddListener<bool>(EEvent.ShowArea, SetVisible);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<bool>(EEvent.ShowArea, SetVisible);
    }

    public void SetRange(Vector3 min, Vector3 max)
    {
        tmp.text = number;
        rectTransform.position = Camera.main.WorldToScreenPoint((max + min) / 2);
        rectTransform.sizeDelta = Camera.main.WorldToScreenPoint(max) - Camera.main.WorldToScreenPoint(min);
    }

    private void SetVisible(bool visible)
    {
        canvas.Visible = visible;
    }
}
