using UnityEngine;

public class RangeManager : MonoBehaviour
{
    public RectInt range;
    public int extend;

    [SerializeField]
    private AreaPanelManager areaManager;
    private LineRenderer lineRenderer;
    private GridGenerator gridGenerator;

    [HideInInspector]
    public DraggableVertex[] vertices;
    public int xMin, xMax, yMin, yMax;

    private int[] xs;
    private int[] ys;
    
    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        gridGenerator = GetComponent<GridGenerator>();
        vertices = GetComponentsInChildren<DraggableVertex>();
        xs = new int[] { range.xMin, xMin, xMax, range.xMax };
        ys = new int[] { range.yMin, yMin, yMax, range.yMax };
        foreach (DraggableVertex vertex in vertices)
        {
            vertex.range = new Rect(range.position, range.size);
        }
    }

    private void Start()
    {
        areaManager.SetAreas(xs, ys);
        gridGenerator.GenerateLine(range, xs, ys, extend * 2 + 2f, false);
        gridGenerator.GenerateGrid(range, extend);
    }

    private void Update()
    {
        Vector3[] points = new Vector3[vertices.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = vertices[i].transform.position;
        }
        lineRenderer.SetPositions(points);
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
