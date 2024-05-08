namespace LiangBarsky
{
    public class Edge_LiangBarsky : Edge
    {
        protected override void AfterRefreshEdge(EdgeData edgeData)
        {
            base.AfterRefreshEdge(edgeData);

            EdgeData_LiangBarsky data = edgeData as EdgeData_LiangBarsky;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].transform.position = data.p1 + data.us[i] * data.r;
                (vertices[i] as Vertex_LiangBarsky).SetU(data.us[i]);
            }
        }
    }
}