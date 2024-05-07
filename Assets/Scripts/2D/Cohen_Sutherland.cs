using UnityEngine;

public class Cohen_Sutherland : MonoBehaviour
{
    [SerializeField]
    private AreaPanelManager areaManager;
    private GridGenerator2D gridGenerator;
    public RectInt range;

    public int xMin, xMax, yMin, yMax;

    private int[] xs;
    private int[] ys;
    
    private void Awake()
    {
        xs = new int[] { range.xMin, xMin, xMax, range.xMax };
        ys = new int[] { range.yMin, yMin, yMax, range.yMax };
        gridGenerator = GetComponent<GridGenerator2D>();
    }

    private void Start()
    {
        areaManager.SetAreas(xs, ys);
        gridGenerator.GenerateLine(range, xs, ys);
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
