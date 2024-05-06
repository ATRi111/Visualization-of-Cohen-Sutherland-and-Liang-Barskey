using UnityEngine;

public class Cohen_Sutherland : MonoBehaviour
{
    [SerializeField]
    private GridGenerator2D gridGenerator;
    [SerializeField]
    private AreaPanelManager areaManager;
    public RectInt range;

    public int xMin, xMax, yMin, yMax;

    private int[] xs;
    private int[] ys;
    private int[] xs_;
    private int[] ys_;
    
    private void Awake()
    {
        gridGenerator.range = range;
        xs = new int[] { xMin, xMax };
        ys = new int[] { yMin, yMax };
        xs_ = new int[] { range.xMin, xMin, xMax, range.xMax };
        ys_ = new int[] { range.yMin, yMin, yMax, range.yMax };
    }

    private void Start()
    {
        gridGenerator.GenerateGrid();
        gridGenerator.GenerateLine(xs, ys);
        areaManager.GenerateAreas(xs_, ys_);
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
