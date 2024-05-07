using Services;
using Services.SceneManagement;
using UnityEngine;

public class Button_LoadScene : ButtonBase
{
    private ISceneController sceneController;
    [SerializeField]
    private string sceneName;

    protected override void Awake()
    {
        base.Awake();
        sceneController = ServiceLocator.Get<ISceneController>();
    }

    protected override void OnClick()
    {
        sceneController.LoadScene(sceneName);
    }
}
