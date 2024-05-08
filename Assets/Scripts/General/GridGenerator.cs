using Services;
using Services.ObjectPools;
using System;
using UnityEngine;

public class GridGenerator : MonoBehaviour
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

    public void GenerateGrid(RectInt range, int extend = 0)
    {
        for (int i = range.xMin - extend; i < range.xMax + extend; i++) 
        {
            for(int j = range.yMin - extend; j < range.yMax + extend; j++)
            {
                objectManager.Activate("Grid", IndexToWorld(i, j), Vector3.zero, transform);
            }
        }
    }

    public void GenerateLine(RectInt range, int[] xs, int[] ys, float extend = 0f, bool containsBorder = true)
    {
        ObjectPoolUtility.RecycleMyObjects(lines);
        if(!containsBorder)
        {
            int[] temp = new int[xs.Length];
            Array.Copy(xs, temp, xs.Length);
            xs = new int[xs.Length - 2];
            Array.Copy(temp, 1, xs, 0, xs.Length);
            Array.Copy(ys, temp, ys.Length);
            ys = new int[ys.Length - 2];
            Array.Copy(temp, 1, ys, 0, ys.Length);
        }
        foreach (int x in xs)
        {
            IMyObject temp = objectManager.Activate("Line", IndexToWorld(x, range.center.y), new Vector3(0f, 0f, 90f), lines.transform);
            temp.Transform.localScale = new Vector3(range.height + extend, 1f, 1f);
        }
        foreach(int y in ys)
        {
            IMyObject temp = objectManager.Activate("Line", IndexToWorld(range.center.x, y), Vector3.zero, lines.transform);
            temp.Transform.localScale = new Vector3(range.width + extend, 1f, 1f);
        }
    }

    private void OnDestroy()
    {
        ObjectPoolUtility.RecycleMyObjects(gameObject);
    }
}
