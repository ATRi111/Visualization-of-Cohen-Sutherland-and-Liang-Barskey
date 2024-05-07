namespace CohenSutherland
{
    public class Edge_CohenSutherland : Edge
    {
        protected override void AfterRefreshEdge(EdgeData edgeData)
        {
            base.AfterRefreshEdge(edgeData);

            EdgeData_CohenSutherland data = edgeData as EdgeData_CohenSutherland;
            (vertex1 as Vertex_CohenSutherland).SetCode(data.code1);
            (vertex2 as Vertex_CohenSutherland).SetCode(data.code2);

            if (data.Cull)
                lineRenderer.enabled = false;
        }
    }
}