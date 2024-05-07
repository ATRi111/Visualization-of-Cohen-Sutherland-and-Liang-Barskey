using UnityEngine;

public class EdgeData
{
    public Vector3 p1, p2;

    public EdgeData(Vector3 p1, Vector3 p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }

    //XY平面内，与X=x这一直线求交
    public Vector2 IntersectX(int x)
    {
        float y = Mathf.Lerp(p1.y, p2.y, (x - p1.x) / (p2.x - p1.x));
        return new Vector2(x, y);
    }
    //XY平面内，与Y=y这一直线求交
    public Vector2 IntersectY(int y)
    {
        float x = Mathf.Lerp(p1.x, p2.x, (y - p1.y) / (p2.y - p1.y));
        return new Vector2(x, y);
    }

    public override string ToString()
    {
        return $"p1:{p1} p2:{p2}";
    }
}