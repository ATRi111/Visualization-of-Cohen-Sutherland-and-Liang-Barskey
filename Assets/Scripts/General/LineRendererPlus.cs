using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererPlus : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Material[] materials;

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
            lineRenderer.enabled = Visible;
            lineRenderer.SetPositions(positions);
        }
    }

    public void SetMaterial(int index)
    {
        lineRenderer.material = materials[index];
    }
}
