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

            DraggableVertex[] vertices = GetComponentsInChildren<DraggableVertex>();
            foreach (DraggableVertex vertex in vertices)
            {
                vertex.range = new Rect(range.position, range.size);
            }
        }

        private void OnEnable()
        {
            eventSystem.AddListener(EEvent.ResetEdge, ResetEdge);
            eventSystem.AddListener(EEvent.Launch, Launch);
            eventSystem.AddListener(EEvent.MoveNext, MoveNext);
        }

        private void OnDisable()
        {
            eventSystem.RemoveListener(EEvent.ResetEdge, ResetEdge);
            eventSystem.RemoveListener(EEvent.Launch, Launch);
            eventSystem.RemoveListener(EEvent.MoveNext, MoveNext);
        }

        private void Start()
        {
            areaManager.SetAreas(xs, ys);
            gridGenerator.GenerateLine(range, xs, ys);
        }

        private void Launch()
        {
            core.Initialize(xMin, yMin, xMax, yMax, draggableVertices[0].transform.position, draggableVertices[1].transform.position);
        }
        private void ResetEdge()
        {
            core.Reset();
            eventSystem.Invoke(EEvent.StateChange, false);
        }
        private void Refresh()
        {
            Debug.Log(core.data);
            eventSystem.Invoke(EEvent.AfterRefreshEdge, core.data as EdgeData);
            eventSystem.Invoke(EEvent.StateChange, core.isRunning && !core.data.CullOff && !core.data.Cull);
        }
        private void MoveNext()
        {
            core.MoveNext();
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