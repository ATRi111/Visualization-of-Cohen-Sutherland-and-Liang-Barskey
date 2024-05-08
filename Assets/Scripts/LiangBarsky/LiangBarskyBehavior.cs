using Services;
using Services.Event;
using UnityEngine;

namespace LiangBarsky
{
    public class LiangBarskyBehavior : MonoBehaviour
    {
        private IEventSystem eventSystem;
        private LiangBarskyCore core;
        private RangeManager rangeManager;

        private void Awake()
        {
            rangeManager = GetComponent<RangeManager>();
            eventSystem = ServiceLocator.Get<IEventSystem>();
            core = new LiangBarskyCore();
            core.Refresh += Refresh;
        }

        private void OnEnable()
        {
            eventSystem.AddListener(EEvent.ResetEdge, ResetEdge);
            eventSystem.AddListener(EEvent.Launch, Launch);
        }

        private void OnDisable()
        {
            eventSystem.RemoveListener(EEvent.ResetEdge, ResetEdge);
            eventSystem.RemoveListener(EEvent.Launch, Launch);
        }

        private void Launch()
        {
            core.Initialize(rangeManager);
        }
        private void ResetEdge()
        {
            core.Reset();
            eventSystem.Invoke(EEvent.HasNext, false);
        }
        private void Refresh()
        {
            eventSystem.Invoke(EEvent.AfterRefreshEdge, core.data as EdgeData);
            eventSystem.Invoke(EEvent.HasNext, core.isRunning);
        }
    }
}