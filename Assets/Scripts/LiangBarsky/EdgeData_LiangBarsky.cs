using System.Collections.Generic;
using UnityEngine;

namespace LiangBarsky
{
    public class EdgeData_LiangBarsky : EdgeData
    {
        public List<float> us;
        public Vector3 r;

        public EdgeData_LiangBarsky(Vector3 p1, Vector3 p2) : base(p1, p2)
        {
            r = p2 - p1;
            us = new List<float> { 0f, 1f };
        }

        public void IntersectAndSort(int xMin, int xMax, int yMin, int yMax)
        {
            if(r.x != 0)
            {
                us.Add((xMin - p1.x) / r.x);
                us.Add((xMax - p1.x) / r.x);
            }
            if(r.y != 0)
            {
                us.Add((yMin - p1.y) / r.y);
                us.Add((yMax - p1.y) / r.y);
            }

            for (int i = 0; i < us.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < us.Count; j++)
                {
                    if (us[j] < us[min])
                        min = j;
                }
                (us[i], us[min]) = (us[min], us[i]);
            }
        }
    }
}