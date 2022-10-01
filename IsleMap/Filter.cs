using ImGuiScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsleMap
{
    internal class Filter
    {
        public int NodeId { get; set; }
        public string NodeName { get; set; } = default!;
        public bool IsNodeVisible { get; set; }
        public bool FilterState { get; set; }

        public Filter() { }
        public Filter(int nodeId, string nodeName, bool isNodeVisible, bool filterState)
        {
            NodeId = nodeId;
            NodeName = nodeName;
            IsNodeVisible = isNodeVisible;
            FilterState = filterState;
        }
    }
}
