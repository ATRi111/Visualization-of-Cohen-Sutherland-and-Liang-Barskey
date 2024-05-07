using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    protected TextMeshProUGUI tmp;
    protected IEventSystem eventSystem;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }
}