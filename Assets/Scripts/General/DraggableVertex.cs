using Services;
using Services.Event;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableVertex : MonoBehaviour, IDragHandler
{
    [HideInInspector]
    public Rect range;
    private bool draggable;
    private IEventSystem eventSystem;

    protected virtual void Awake()
    {
        draggable = true;
        eventSystem = ServiceLocator.Get<IEventSystem>();
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
        draggable = true;
    }

    private void AfterRefreshEdge(EdgeData _)
    {
        draggable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!draggable) 
            return;
        float z = transform.position.z;
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.ResetZ(z);
        if(range.Contains(temp))
            transform.position = new Vector3(temp.x, temp.y, z);
    }
}
