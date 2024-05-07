using TMPro;

public class Button_ShowArea : ButtonBase
{
    private TextMeshProUGUI tmp;
    private bool visible;

    protected override void Awake()
    {
        base.Awake();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        visible = false;
    }

    protected override void OnClick()
    {
        visible = !visible;
        eventSystem.Invoke(Services.EEvent.ShowArea, visible);
        tmp.text = visible ? "隐藏区域" : "显示区域";
    }
}
