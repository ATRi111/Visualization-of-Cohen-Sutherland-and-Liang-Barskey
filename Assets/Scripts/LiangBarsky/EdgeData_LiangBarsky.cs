using System.Collections.Generic;
using UnityEngine;

namespace LiangBarsky
{
    public class EdgeData_LiangBarsky : EdgeData
    {
        public List<float> ins;
        public List<float> outs;
        public Vector3 r;
        public List<Vector3> result;
        public List<Vector3> points;

        public EdgeData_LiangBarsky(Vector3 p1, Vector3 p2) : base(p1, p2)
        {
            r = p2 - p1;
            ins = new List<float> { 0f };
            outs = new List<float> { 1f };
            result = new List<Vector3>();
            points = new List<Vector3> { p1, p2 };
        }

        public float UX(float x)
            => (x - p1.x) / r.x;
        public float UY(float y)
            => (y - p1.y) / r.y;
        public Vector3 R(float u)
            => p1 + u * r;

        public void Intersect(int xMin, int xMax, int yMin, int yMax)
        {
            float u1, u2;
            void AddPair()
            {
                if (u1 > u2)
                    (u1, u2) = (u2, u1);
                ins.Add(u1);
                outs.Add(u2);
                points.Add(R(u1));
                points.Add(R(u2));
            }

            u1 = UX(xMin);
            u2 = UX(xMax);
            AddPair();
            u1 = UY(yMin);
            u2 = UY(yMax);
            AddPair();

            ins.Sort(); 
            outs.Sort();

            if (ins[^1] < outs[0])
            {
                result.Add(R(ins[^1]));
                result.Add(R(outs[0]));
            }
        }
    }
}