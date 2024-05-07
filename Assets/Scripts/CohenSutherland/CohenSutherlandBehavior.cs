using Services;
using Services.Event;
using UnityEngine;

namespace CohenSutherland
{
    public class CohenSutherlandBehavior : MonoBehaviour
    {
        public static CohenSutherlandBehavior FindInstance()
        {
            return GameObject.Find(nameof(CohenSutherlandBehavior)).GetComponent<CohenSutherlandBehavior>();
        }

        [SerializeField]
        private AreaPanelManager areaManager;
        private GridGenerator gridGenerator;
        private IEventSystem eventSystem;
        private DraggableVertex[] draggableVertices;

        private CohenSutherlandCore core;
        public RectInt range;

        public int xMin, xMax, yMin, yMax;

        private int[] xs;
        private int[] ys;

        private void Awake()
        {
            xs = new int[] { range.xMin, xMin, xMax, range.xMax };
            ys = new int[] { range.yMin, yMin, yMax, range.yMax };
            eventSystem = ServiceLocator.Get<IEventSystem>();
            gridGenerator = GetComponent<GridGenerator>();
            core = new CohenSutherlandCore();
            draggableVertices = GetComponentsInChildren<DraggableVertex>();
            core.Refresh += Refresh;
        }

        private void OnEnable()
        {
            eventSystem.AddListener(EEvent.ResetEdge, AfterResetEdge);
            eventSystem.AddListener(EEvent.Launch, Launch);
        }

        private void OnDisable()
        {
            eventSystem.RemoveListener(EEvent.ResetEdge, AfterResetEdge);
            eventSystem.RemoveListener(EEvent.Launch, Launch);
        }

        private void Start()
        {
            areaManager.SetAreas(xs, ys);
            gridGenerator.GenerateLine(range, xs, ys);
        }

        public void Launch()
        {
            core.Initialize(xMin, yMin, xMax, yMax, draggableVertices[0].transform.position, draggableVertices[1].transform.position);
        }

        private void AfterResetEdge()
        {
            core.Reset();
        }

        private void Refresh()
        {
            eventSystem.Invoke(EEvent.RefreshEdge, core.data as EdgeData);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(range.center, new Vector3(range.size.x, range.size.y, 1f));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(xMin, range.yMin), new Vector3(xMin, range.yMax));
            Gizmos.DrawLine(new Vector3(xMax, range.yMin), new Vector3(xMax, range.yMax));
            Gizmos.DrawLine(new Vector3(range.xMin, yMin), new Vector3(range.xMax, yMin));
            Gizmos.DrawLine(new Vector3(range.xMin, yMax), new Vector3(range.xMax, yMax));
        }
    }
}