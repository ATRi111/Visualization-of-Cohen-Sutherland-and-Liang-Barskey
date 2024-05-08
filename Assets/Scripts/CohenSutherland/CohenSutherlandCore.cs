using System;
using UnityEngine;

namespace CohenSutherland
{
    public class CohenSutherlandCore
    {
        public static int[] Ops;
        static CohenSutherlandCore()
        {
            Ops = new int[] { 0, 0b1000, 0b0100, 0b0010, 0b0001 };
        }

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
            int code;
            Vector3 p = default;

            if (data.code1 != 0)
                code = data.code1;
            else
                code = data.code2;

            p = Intersects[code & (~code + 1)]();   //计算code最后一位1
            code = Calculate(p);

            if (data.code1 != 0)
            {
                data.code1 = code;
                data.p1 = p;
            }
            else
            {
                data.code2 = code;
                data.p2 = p;
            }
            Refresh?.Invoke();
        }

        public int Calculate(Vector2 p)
        {
            int code = 0;
            if (p.y > yMax)
                code |= Ops[1];
            if (p.y < yMin)
                code |= Ops[2];
            if (p.x > xMax)
                code |= Ops[3];
            if (p.x < xMin)
                code |= Ops[4];
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