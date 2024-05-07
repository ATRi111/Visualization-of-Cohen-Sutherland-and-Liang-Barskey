using System;
using TMPro;
using UnityEngine;

namespace CohenSutherland
{
    public class Vertex_CohenSutherland : Vertex
    {
        [SerializeField]
        protected TextMeshProUGUI tmp;

        public void SetCode(int code)
        {
            tmp.text = Convert.ToString(code, 2).PadLeft(4, '0');
        }
    }
}