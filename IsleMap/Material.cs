using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace IsleMap
{
    internal class Material
    {
        public string NodeName { get; set; } = default!;
        public string MainMat { get; set; } = default!;
        public string AltMat { get; set; } = default!;
        public int X { get; set; }
        public int Y { get; set; }
        public Material() { }
        public Material(string node_name, string main_mat, string alt_mat, int x, int y)
        {
            NodeName = node_name;
            MainMat = main_mat;
            AltMat = alt_mat;
            X = x;
            Y = y;
        }
    }
}
