using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class Edge : MonoBehaviour
{
    protected IEventSystem eventSystem;
    protected LineRenderer lineRenderer;
    public Vertex vertex1, vertex2;

    protected TextMeshProUGUI tmp;

    protected virtual void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        eventSystem = ServiceLocator.Get<IEventSystem>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
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
        lineRenderer.enabled = false;
    }

    protected virtual void AfterRefreshEdge(EdgeData edgeData)
    {
        vertex1.transform.position = edgeData.p1;
        vertex2.transform.position = edgeData.p2;
        Vector3[] line = new Vector3[] { vertex1.transform.position, vertex2.transform.position };
        lineRenderer.enabled = true;
        lineRenderer.SetPositions(line);
    }
}