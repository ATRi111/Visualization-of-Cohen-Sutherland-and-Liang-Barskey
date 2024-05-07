using Services;
using Services.Event;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableVertex : MonoBehaviour, IDragHandler
{
    private IEventSystem eventSystem;
    private SpriteRenderer spriteRenderer;
    private GameObject canvas;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>().gameObject;
    }

    private void OnEnable()
    {
        eventSystem.AddListener<EdgeData>(EEvent.RefreshEdge, AfterRefreshEdge);
        eventSystem.AddListener(EEvent.ResetEdge, AfterResetEdge);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<EdgeData>(EEvent.RefreshEdge, AfterRefreshEdge);
        eventSystem.RemoveListener(EEvent.ResetEdge, AfterResetEdge);
    }

    private void AfterResetEdge()
    {
        spriteRenderer.enabled = true;
        canvas.SetActive(true);
    }

    private void AfterRefreshEdge(EdgeData _)
    {
        spriteRenderer.enabled = false;
        canvas.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float z = transform.position.z;
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(temp.x, temp.y, z);
    }
}