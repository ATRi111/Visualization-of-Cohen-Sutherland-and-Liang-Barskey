using System;
using UnityEngine;

namespace CohenSutherland
{
    public class CohenSutherlandCore
    {
        public static int[] Ops;
        static CohenSutherlandCore()
        {
            Ops = new int[] { 0b1111, 0b1000, 0b0100, 0b0010, 0b0001 };
        }

        public Action Refresh;
        public Func<Vector2>[] Inserts;
        public int yMax, yMin, xMax, xMin;

        public EdgeData_CohenSutherland data;
        public bool isRunning;

        public void Reset()
        {
            isRunning = false;
        }

        public void Initialize(int xMin, int yMin, int xMax, int yMax, Vector2 p1, Vector2 p2)
        {
            isRunning = true;
            this.xMin = xMin;
            this.yMin = yMin;
            this.xMax = xMax;
            this.yMax = yMax;
            data = new EdgeData_CohenSutherland(p1, p2, Calculate(p1), Calculate(p2));
            Inserts = new Func<Vector2>[] { null, Intersect1, Intersect2, Intersect3, Intersect4 };
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

            for (int i = Ops.Length - 1; i > 0; i--)
            {
                if ((code & Ops[i]) != 0)
                {
                    p = Inserts[i].Invoke();
                    code = Calculate(p);
                    break;
                }
            }

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

        public Vector2 Intersect1()
            => data.IntersectY(yMax);
        public Vector2 Intersect2()
            => data.IntersectY(yMin);
        public Vector2 Intersect3()
            => data.IntersectX(xMax);
        public Vector2 Intersect4()
            => data.IntersectX(yMin);
    }
}