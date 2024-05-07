using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class Edge : MonoBehaviour
{
    protected IEventSystem eventSystem;

    public Vertex vertex1, vertex2;

    protected TextMeshProUGUI tmp;
    protected SpriteRenderer[] spriteRenderers;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void OnEnable()
    {
        eventSystem.AddListener<EdgeData>(EEvent.RefreshEdge, AfterRefreshEdge);
        eventSystem.AddListener(EEvent.ResetEdge, AfterResetEdge);
    }

    protected virtual void OnDisable()
    {
        eventSystem.RemoveListener<EdgeData>(EEvent.RefreshEdge, AfterRefreshEdge);
        eventSystem.RemoveListener(EEvent.ResetEdge, AfterResetEdge);
    }

    protected virtual void AfterResetEdge()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = false;
        }
    }

    protected virtual void AfterRefreshEdge(EdgeData edgeData)
    {
        vertex1.transform.position = edgeData.p1;
        vertex2.transform.position = edgeData.p2;
       
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = true;
        }
    }
}