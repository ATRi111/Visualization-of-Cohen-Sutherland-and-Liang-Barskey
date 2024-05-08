using System;
using UnityEngine;

namespace CohenSutherland
{
    public class CohenSutherlandCore
    {
        public Action Refresh;
        public Func<Vector2>[] Intersects;
        public int yMax, yMin, xMax, xMin;

        public EdgeData_CohenSutherland data;
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
            data = new EdgeData_CohenSutherland(p1, p2, Calculate(p1), Calculate(p2));
            Intersects = new Func<Vector2>[9];
            Intersects[1] = Intersect0001;
            Intersects[2] = Intersect0010;
            Intersects[4] = Intersect0100;
            Intersects[8] = Intersect1000;
            Refresh?.Invoke();
        }

        public void MoveNext()
        {
            if(data.code1 != 0)
            {
                data.p1 = Intersects[data.code1 & (~data.code1 + 1)](); 
                data.code1 = Calculate(data.p1);
            }
            else
            {
                data.p2 = Intersects[data.code2 & (~data.code2 + 1)](); 
                data.code2 = Calculate(data.p2);
            }
            Refresh?.Invoke();
        }

        public int Calculate(Vector2 p)
        {
            int code = 0;
            if (p.y > yMax)
                code |= 8;
            if (p.y < yMin)
                code |= 4;
            if (p.x > xMax)
                code |= 2;
            if (p.x < xMin)
                code |= 1;
            return code;
        }

        public Vector2 Intersect1000()
            => data.IntersectY(yMax);
        public Vector2 Intersect0100()
            => data.IntersectY(yMin);
        public Vector2 Intersect0010()
            => data.IntersectX(xMax);
        public Vector2 Intersect0001()
            => data.IntersectX(xMin);
    }
}