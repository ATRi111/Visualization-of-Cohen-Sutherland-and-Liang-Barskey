using UnityEngine;

namespace CohenSutherland
{
    public class EdgeData_CohenSutherland : EdgeData
    {
        public int code1, code2;

        public EdgeData_CohenSutherland(Vector3 p1, Vector3 p2, int code1, int code2) : base(p1, p2)
        {
            this.code1 = code1;
            this.code2 = code2;
        }

        public bool Cull => (code1 & code2) != 0;
        public bool CullOff => (code1 | code2) == 0;

        public override string ToString()
        {
            return base.ToString() + $" Cull:{Cull} CullOff:{CullOff}";
        }
    }
}