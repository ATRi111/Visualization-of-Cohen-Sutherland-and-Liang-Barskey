using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AreaPanel : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    private RectTransform rectTransform;
    private CanvasGrounpPlus canvas;
    private TextMeshProUGUI tmp;
    [HideInInspector]
    public string number;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGrounpPlus>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetRange(Vector3 min, Vector3 max)
    {
        canvas.Visible = true;
        tmp.text = number;
        rectTransform.position = Camera.main.WorldToScreenPoint((max + min) / 2);
        rectTransform.sizeDelta = Camera.main.WorldToScreenPoint(max) - Camera.main.WorldToScreenPoint(min);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvas.Visible = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvas.Visible = true;
    }
}
