using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Common.Configuration;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace IsleMap.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;
    
    public ConfigWindow(Plugin plugin) : base(
        "Filter Nodes",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(360, 660);
        this.SizeCondition = ImGuiCond.Always;       

        this.Configuration = plugin.Configuration;
    }
    public void Dispose() { }

    public override void Draw()
    {
        var node1 = this.Configuration.node1;
        var node2 = this.Configuration.node2;
        var node3 = this.Configuration.node3;
        var node4 = this.Configuration.node4;
        var node5 = this.Configuration.node5;
        var node6 = this.Configuration.node6;
        var node7 = this.Configuration.node7;
        var node8 = this.Configuration.node8;
        var node9 = this.Configuration.node9;
        var node10 = this.Configuration.node10;
        var node11 = this.Configuration.node11;
        var node12 = this.Configuration.node12;
        var node13 = this.Configuration.node13;
        var node14 = this.Configuration.node14;
        var node15 = this.Configuration.node15;
        var node16 = this.Configuration.node16;
        var node17 = this.Configuration.node17;
        var node18 = this.Configuration.node18;
        var node19 = this.Configuration.node19;
        var node20 = this.Configuration.node20;
        var node21 = this.Configuration.node21;
        var node22 = this.Configuration.node22;
        var node23 = this.Configuration.node23;

        if (ImGui.Checkbox("Select/Deselect All: All materials/No materials", ref node1))
        {
            this.Configuration.node1 = node1;
            this.Configuration.node2 = node1;
            this.Configuration.node3 = node1;
            this.Configuration.node4 = node1;
            this.Configuration.node5 = node1;
            this.Configuration.node6 = node1;
            this.Configuration.node7 = node1;
            this.Configuration.node8 = node1;
            this.Configuration.node9 = node1;
            this.Configuration.node10 = node1;
            this.Configuration.node11 = node1;
            this.Configuration.node12 = node1;
            this.Configuration.node13 = node1;
            this.Configuration.node14 = node1;
            this.Configuration.node15 = node1;
            this.Configuration.node16 = node1;
            this.Configuration.node17 = node1;
            this.Configuration.node18 = node1;
            this.Configuration.node19 = node1;
            this.Configuration.node20 = node1;
            this.Configuration.node21 = node1;
            this.Configuration.node22 = node1;
            this.Configuration.node23 = node1;
            this.Configuration.Save();
        }              
        if (ImGui.Checkbox("Island Apple Tree: Island Apple and Island Vine", ref node2))
        {
            this.Configuration.node2 = node2;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Tualong Tree: Island Branch and Island Log", ref node3))
        {
            this.Configuration.node3 = node3;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Partially Consumed Cabbage: Island Cabbage Seed", ref node4))
        {
            this.Configuration.node4 = node4;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Large Shell: Island Clam and Islefish", ref node5))
        {
            this.Configuration.node5 = node5;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Mound of Dirt: Island Clay and Island Sand", ref node6))
        {
            this.Configuration.node6 = node6;
            this.Configuration.Save();
        }      
        if (ImGui.Checkbox("Bluish Rock: Island Copper Ore and Island Stone", ref node7))
        {
            this.Configuration.node7 = node7;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Cotton Plant: Island Cotton Boll and Islewort", ref node8))
        {
            this.Configuration.node8 = node8;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Agave Plant: Island Hemp and Islewort", ref node9))
        {
            this.Configuration.node9 = node9;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Rough Black Rock: Island Iron Ore and Island Stone", ref node10))
        {
            this.Configuration.node10 = node10;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Coral Formation: Island Jellyfish and Island Coral", ref node11))
        {
            this.Configuration.node11 = node11;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Speckled Rock: Island Leucogranite and Island Stone", ref node12))
        {
            this.Configuration.node12 = node12;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Smooth White Rock: Island Limestone and Island Stone", ref node13))
        {
            this.Configuration.node13 = node13;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Palm Tree: Island Palm Log and Island Palm Leaf", ref node14))
        {
            this.Configuration.node14 = node14;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Wild Parsnip: Island Parsnip Seeds and Islewort", ref node15))
        {
            this.Configuration.node15 = node15;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Wild Popoto: Island Popoto Set and Islewort", ref node16))
        {
            this.Configuration.node16 = node16;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Lightly Gnawed Pumpkin: Island Pumpkin Seed", ref node17))
        {
            this.Configuration.node17 = node17;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Quartz Formation: Island Quartz and Island Stone", ref node18))
        {
            this.Configuration.node18 = node18;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Crystal-banded Rock: Island Rock Salt and Island Stone", ref node19))
        {
            this.Configuration.node19 = node19;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Mahogany Tree: Island Sap and Island Log", ref node20))
        {
            this.Configuration.node20 = node20;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Seaweed Tangle: Island Squid and Island Laver", ref node21))
        {
            this.Configuration.node21 = node21;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Sugarcane: Island Sugarcane and Island Vine", ref node22))
        {
            this.Configuration.node22 = node22;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Submerged Sand: Island Tinsand and Island Sand", ref node23))
        {
            this.Configuration.node23 = node23;
            this.Configuration.Save();
        }
    }
}
