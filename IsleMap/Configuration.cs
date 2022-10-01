using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace IsleMap
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;
        public bool node1 { get; set; } = false;
        public bool node2 { get; set; } = false;
        public bool node3 { get; set; } = false;
        public bool node4 { get; set; } = false;
        public bool node5 { get; set; } = false;
        public bool node6 { get; set; } = false;
        public bool node7 { get; set; } = false;
        public bool node8 { get; set; } = false;
        public bool node9 { get; set; } = false;
        public bool node10 { get; set; } = false;
        public bool node11 { get; set; } = false;
        public bool node12 { get; set; } = false;
        public bool node13 { get; set; } = false;
        public bool node14 { get; set; } = false;
        public bool node15 { get; set; } = false;
        public bool node16 { get; set; } = false;
        public bool node17 { get; set; } = false;
        public bool node18 { get; set; } = false;
        public bool node19 { get; set; } = false;
        public bool node20 { get; set; } = false;
        public bool node21 { get; set; } = false;
        public bool node22 { get; set; } = false;
        public bool node23 { get; set; } = false;

        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.PluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
