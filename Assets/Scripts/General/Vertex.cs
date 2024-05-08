using Services;
using Services.Event;
using UnityEngine;
using UnityEngine.UI;

public class Vertex : MonoBehaviour
{
    protected IEventSystem eventSystem;
    protected GameObject canvas;
    protected SpriteRenderer spriteRenderer;
    protected Image image;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        canvas = GetComponentInChildren<Canvas>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        eventSystem.AddListener<EdgeData>(EEvent.AfterRefreshEdge, AfterRefreshEdge);
        eventSystem.AddListener(EEvent.ResetEdge, AfterResetEdge);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<EdgeData>(EEvent.AfterRefreshEdge, AfterRefreshEdge);
        eventSystem.RemoveListener(EEvent.ResetEdge, AfterResetEdge);
    }

    private void AfterResetEdge()
    {
        spriteRenderer.enabled = false;
        canvas.SetActive(false);
    }

    private void AfterRefreshEdge(EdgeData _)
    {
        spriteRenderer.enabled = true;
        canvas.SetActive(true);
    }
}