using System;
using UnityEngine;

namespace LiangBarsky
{
    public class LiangBarskyCore : MonoBehaviour
    {
        public Action Refresh;
        public int yMax, yMin, xMax, xMin;

        public EdgeData_LiangBarsky data;
        public bool isRunning;

        public void Reset()
        {
            isRunning = false;
        }

        public void Initialize(RangeManager rangeManager)
        {
            isRunning = true;
            xMin = rangeManager.xMin;
            yMin = rangeManager.yMin;
            xMax = rangeManager.xMax;
            yMax = rangeManager.yMax;
            Vector3 p1 = rangeManager.vertices[0].transform.position;
            Vector3 p2 = rangeManager.vertices[1].transform.position;
            data = new EdgeData_LiangBarsky(p1, p2);
            data.IntersectAndSort(xMin, xMax, yMin, yMax);
            Refresh?.Invoke();
        }
    }
}