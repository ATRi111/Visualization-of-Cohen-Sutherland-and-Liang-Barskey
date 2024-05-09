using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class Edge : MonoBehaviour
{
    protected IEventSystem eventSystem;
    protected LineRendererPlus lineRenderer;
    protected Vertex[] vertices;

    protected TextMeshProUGUI tmp;

    protected virtual void Awake()
    {
        lineRenderer = GetComponent<LineRendererPlus>();
        eventSystem = ServiceLocator.Get<IEventSystem>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        vertices = GetComponentsInChildren<Vertex>();
    }

    protected virtual void OnEnable()
    {
        eventSystem.AddListener<EdgeData>(EEvent.AfterRefreshEdge, AfterRefreshEdge);
        eventSystem.AddListener(EEvent.ResetEdge, AfterResetEdge);
    }

    protected virtual void OnDisable()
    {
        eventSystem.RemoveListener<EdgeData>(EEvent.AfterRefreshEdge, AfterRefreshEdge);
        eventSystem.RemoveListener(EEvent.ResetEdge, AfterResetEdge);
    }

    protected virtual void AfterResetEdge()
    {
        lineRenderer.Visible = false;
    }

    protected virtual void AfterRefreshEdge(EdgeData edgeData)
    {
        lineRenderer.Visible = true;
    }
}