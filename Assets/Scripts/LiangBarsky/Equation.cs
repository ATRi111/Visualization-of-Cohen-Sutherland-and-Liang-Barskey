using System.Text;
using TMPro;
using UnityEngine;

public class Equation : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    [SerializeField]
    private DraggableVertex p1, p2;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Vector2 r = p2.transform.position - p1.transform.position;
        Vector2 r0 = p1.transform.position;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("参数方程");
        sb.AppendLine($"x={r.x:f1}u{r0.x:+0.0;-0.0}");
        sb.AppendLine($"y={r.y:f1}u{r0.y:+0.0;-0.0}");
        tmp.text = sb.ToString();
    }
}
