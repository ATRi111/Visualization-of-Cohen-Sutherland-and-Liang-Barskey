using Services;
using Services.ObjectPools;
using UnityEngine;

public class GridGenerator2D : MonoBehaviour
{
    public static Vector3 IndexToWorld(float x, float y)
    {
        return new Vector3(x, y, 0);
    }

    private IObjectManager objectManager;
    private GameObject lines;
    
    private void Awake()
    {
        objectManager = ServiceLocator.Get<IObjectManager>();
        lines = new GameObject("Lines");
        lines.transform.SetParent(transform);
        lines.transform.localPosition = Vector3.zero;
    }

    public void GenerateGrid(RectInt range)
    {
        for (int i = range.xMin; i < range.xMax; i++) 
        {
            for(int j = range.yMin; j < range.yMax; j++)
            {
                objectManager.Activate("Grid", IndexToWorld(i, j), Vector3.zero, transform);
            }
        }
    }

    public void GenerateLine(RectInt range, int[] xs, int[] ys)
    {
        ObjectPoolUtility.RecycleMyObjects(lines);
        foreach (int x in xs)
        {
            IMyObject temp = objectManager.Activate("Line", IndexToWorld(x, range.center.y), new Vector3(0f, 0f, 90f), lines.transform);
            temp.Transform.localScale = new Vector3(range.height, 1f, 1f);
        }
        foreach(int y in ys)
        {
            IMyObject temp = objectManager.Activate("Line", IndexToWorld(range.center.x, y), Vector3.zero, lines.transform);
            temp.Transform.localScale = new Vector3(range.width, 1f, 1f);
        }
    }

    private void OnDestroy()
    {
        ObjectPoolUtility.RecycleMyObjects(gameObject);
    }
}
