using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererPlus : MonoBehaviour
{
    public static Vector2 Rotate(Vector2 v, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        float x = v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle);
        float y = v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle);
        return new Vector2(x,y);
    }

    private LineRenderer lineRenderer;
    public Material[] materials;

    [SerializeField]
    private float arrowLength;

    private bool visible;
    public bool Visible
    {
        get => visible;
        set
        {
            visible = value;
            lineRenderer.enabled = value;
        }
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPositions(Vector3[] positions)
    {
        if(positions == null || positions.Length == 0)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            if(arrowLength > 0)
            {
                Vector3[] temp = new Vector3[positions.Length + 3];
                Array.Copy(positions,temp,positions.Length);
                Vector3 v = temp[0] - temp[1];
                Vector3 arrow = arrowLength * Rotate(v, 45f).normalized;
                temp[2] = temp[1] + arrow;
                temp[3] = temp[1];
                arrow = arrowLength * Rotate(v, -45f).normalized;
                temp[4] = temp[1] + arrow;
                lineRenderer.positionCount = temp.Length;
                lineRenderer.SetPositions(temp);
            }
            else
            {
                lineRenderer.positionCount = positions.Length;
                lineRenderer.SetPositions(positions);
            }
            lineRenderer.enabled = Visible;
        }
    }

    public void SetMaterial(int index)
    {
        lineRenderer.material = materials[index];
    }
}
