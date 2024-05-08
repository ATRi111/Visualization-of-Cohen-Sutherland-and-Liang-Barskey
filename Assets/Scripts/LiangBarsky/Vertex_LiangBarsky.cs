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
            if (color == Color.clear)
                image.color = Color.clear;
            else
                image.color = Color.white;
        }

        public void SetU(float u)
        {
            tmp.text = $"u={u:f2}";
        }
    }
}