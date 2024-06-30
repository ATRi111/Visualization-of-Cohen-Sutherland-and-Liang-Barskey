using MyTimer;
using UnityEngine;

namespace LiangBarsky
{
    public class Edge_LiangBarsky : Edge
    {
        private class VertexAnim : Metronome
        {
            private EdgeData_LiangBarsky data;
            private Vertex[] vertices;
            private LineRendererPlus lineRenderer;
            private readonly int[] order;
            public int count;

            public VertexAnim()
            {
                order = new int[] { 0, 1, 5, 4};
            }

            public void Initialize(float duration, EdgeData_LiangBarsky data, LineRendererPlus lineRenderer, Vertex[] vertices, bool start = true)
            {
                base.Initialize(duration, start);
                this.data = data;
                this.vertices = vertices;
                this.lineRenderer = lineRenderer;
                count = -2;
            }

            protected override void AfterComplete_(float _)
            {
                if (count < 5) 
                {
                    if(count < 4 && count >= 0)
                        vertices[order[count]].Visible = false;
                    count++;
                    Restart(true);
                }
                else
                {
                    lineRenderer.SetMaterial(1);
                    lineRenderer.SetPositions(data.result.ToArray());
                }
            }
        }

        private LinearTransformation linear;
        private VertexAnim anim;
        private EdgeData_LiangBarsky data;
        private RangeManager rangeManager;
        public Color inColor;
        public Color outColor;

        protected override void Awake()
        {
            base.Awake();
            linear = new LinearTransformation();
            anim = new VertexAnim();
            linear.OnTick += OnTick_Linear;
            linear.AfterCompelete += AfterComplete_Linear;
            rangeManager = GameObject.Find(nameof(LiangBarskyBehavior)).GetComponent<RangeManager>();
        }

        protected override void AfterRefreshEdge(EdgeData edgeData)
        {
            base.AfterRefreshEdge(edgeData);
            data = edgeData as EdgeData_LiangBarsky;
            int count = 0;
            SetColor(float.MinValue);
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

        private void OnTick_Linear(float u)
        {
            lineRenderer.Visible = true;
            lineRenderer.SetMaterial(0);
            SetColor(u);
            Vector3[] points = new Vector3[] { data.R(linear.Origin), data.R(u) };
            lineRenderer.SetPositions(points);
        }

        private void AfterComplete_Linear(float u)
        {
            SetColor(float.MaxValue);
            anim.Initialize(0.5f, data, lineRenderer, vertices);
        }

        private void SetColor(float u)
        {
            int count = 0;
            for (int i = 0; i < data.ins.Count; i++)
            {
                (vertices[count] as Vertex_LiangBarsky).SetColor(data.ins[i] >= u ? Color.clear : inColor);
                count++;
            }
            for (int i = 0; i < data.outs.Count; i++)
            {
                (vertices[count] as Vertex_LiangBarsky).SetColor(data.outs[i] >= u ? Color.clear : outColor);
                count++;
            }
        }
    }
}