using MyTimer;
using UnityEngine;

namespace LiangBarsky
{
    public class Edge_LiangBarsky : Edge
    {
        private LinearTransformation linear;
        private EdgeData_LiangBarsky data;
        private RangeManager rangeManager;
        public Color inColor;
        public Color outColor;

        protected override void Awake()
        {
            base.Awake();
            linear = new LinearTransformation();
            linear.OnTick += OnTick;
            linear.AfterCompelete += AfterComplete;
            rangeManager = GameObject.Find(nameof(LiangBarskyBehavior)).GetComponent<RangeManager>();
        }

        protected override void AfterRefreshEdge(EdgeData edgeData)
        {
            base.AfterRefreshEdge(edgeData);
            data = edgeData as EdgeData_LiangBarsky;
            for (int i = 0; i < vertices.Length; i++)
            {
                (vertices[i] as Vertex_LiangBarsky).SetColor(Color.clear);
            }
            int count = 0;
            for (int i = 0; i < data.ins.Count; i++)
            {
                vertices[count].transform.position = data.R(data.ins[i]);
                (vertices[count] as Vertex_LiangBarsky).SetU(data.ins[i]);
                count++;
            }
            for (int i = 0; i < data.outs.Count; i++)
            {
                vertices[count].transform.position = data.R(data.outs[i]);
                (vertices[count] as Vertex_LiangBarsky).SetU(data.outs[i]);
                count++;
            }
            SetLineRange();
            lineRenderer.enabled = true;
        }

        private void SetLineRange()
        {
            RectInt range = rangeManager.range;
            float min = float.MaxValue;
            float max = float.MinValue;
            Vector2 extend = rangeManager.extend  * Vector2.one;
            Rect rect = new Rect(range.position - extend, range.size + 3 * extend);
            Rect extra = new Rect(rect.position - Vector2.one, rect.size + 2 * Vector2.one);
            void Verify(float u)
            {
                Vector2 v = data.R(u);
                if (extra.Contains(v))
                {
                    min = Mathf.Min(min, u);
                    max = Mathf.Max(max, u);
                }
            }

            if (data.r.x != 0)
            {
                Verify(data.UX(rect.xMin));
                Verify(data.UX(rect.xMax));
            }
            if (data.r.y != 0)
            {
                Verify(data.UY(rect.yMin));
                Verify(data.UY(rect.yMax));
            }
            linear.Initialize(min, max, 2f);
        }

        private void OnTick(float u)
        {
            SetColor(u);
            Vector3[] points = new Vector3[] { data.R(linear.Origin), data.R(u) };
            lineRenderer.SetPositions(points);
        }

        private void AfterComplete(float u)
        {
            SetColor(u);
            if(data.result.Count == 2)
                lineRenderer.SetPositions(data.result.ToArray());
            else
                lineRenderer.enabled = false;
        }

        private void SetColor(float u)
        {
            int count = 0;
            for (int i = 0; i < data.ins.Count; i++)
            {
                if (data.ins[i] > u)
                    break;
                (vertices[count] as Vertex_LiangBarsky).SetColor(inColor);
                count++;
            }
            for (int i = 0; i < data.ins.Count; i++)
            {
                if (data.outs[i] > u)
                    break;
                (vertices[count] as Vertex_LiangBarsky).SetColor(outColor);
                count++;
            }
        }
    }
}