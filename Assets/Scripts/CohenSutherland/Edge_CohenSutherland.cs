using UnityEngine;

namespace CohenSutherland
{
    public class Edge_CohenSutherland : Edge
    {
        protected override void AfterRefreshEdge(EdgeData edgeData)
        {
            base.AfterRefreshEdge(edgeData);

            vertices[0].transform.position = edgeData.p1;
            vertices[1].transform.position = edgeData.p2;
            Vector3[] line = new Vector3[] { vertices[0].transform.position, vertices[1].transform.position };

            EdgeData_CohenSutherland data = edgeData as EdgeData_CohenSutherland;
            (vertices[0] as Vertex_CohenSutherland).SetCode(data.code1);
            (vertices[1] as Vertex_CohenSutherland).SetCode(data.code2);

            lineRenderer.enabled = !data.Cull;
            lineRenderer.SetPositions(line);
        }
    }
}