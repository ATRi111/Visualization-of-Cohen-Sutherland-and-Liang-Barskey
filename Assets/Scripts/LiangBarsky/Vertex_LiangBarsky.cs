using TMPro;
using UnityEngine;

namespace LiangBarsky
{
    public class Vertex_LiangBarsky : Vertex
    {
        [SerializeField]
        protected TextMeshProUGUI tmp;

        public void SetColor(Color color)
        {
            tmp.color = color;
            spriteRenderer.color = color;
        }

        public void SetU(float u)
        {
            tmp.text = $"u={u}";
        }
    }
}