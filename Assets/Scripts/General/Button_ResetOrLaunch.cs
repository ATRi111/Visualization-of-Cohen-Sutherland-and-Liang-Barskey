using TMPro;

public class Button_ResetOrLaunch : ButtonBase
{
    private bool isRunning;
    private TextMeshProUGUI tmp;

    protected override void Awake()
    {
        base.Awake();
        isRunning = false;
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnClick()
    {
        isRunning = !isRunning;
        if(isRunning)
        {
            tmp.text = "重置";
            eventSystem.Invoke(Services.EEvent.Launch);
        }
        else
        {
            tmp.text = "开始裁剪";
            eventSystem.Invoke(Services.EEvent.ResetEdge);
        }
    }
}
