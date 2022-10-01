using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using System.Reflection;
using Dalamud.Interface.Windowing;
using IsleMap.Windows;
using System.Collections.Generic;
using ImGuiScene;

namespace IsleMap
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Isle Map";
        private const string CommandName = "/islemap";

        private DalamudPluginInterface PluginInterface { get; init; }
        private CommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("IsleMap");

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager)
        {
            this.PluginInterface = pluginInterface;
            this.CommandManager = commandManager;

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            var mapPath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "islandsanctuarymap.jpg");
            var mapImage = this.PluginInterface.UiBuilder.LoadImage(mapPath);
            Dictionary<string, TextureWrap> mapIcons = new Dictionary<string, TextureWrap>();          

            //map icon list
            mapIcons.Add("Island Apple Tree", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandapple.png")));
            mapIcons.Add("Tualong Tree", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandbranch.png")));
            mapIcons.Add("Partially Consumed Cabbage", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandcabbageseeds.png")));
            mapIcons.Add("Large Shell", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandclam.png")));
            mapIcons.Add("Mound of Dirt", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandclay.png")));
            mapIcons.Add("Bluish Rock", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandcopperore.png")));
            mapIcons.Add("Cotton Plant", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandcottonboll.png")));
            mapIcons.Add("Agave Plant", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandhemp.png")));
            mapIcons.Add("Rough Black Rock", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandironore.png")));
            mapIcons.Add("Coral Formation", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandjellyfish.png")));
            mapIcons.Add("Speckled Rock", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandleucogranite.png")));
            mapIcons.Add("Smooth White Rock", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandlimestone.png")));
            mapIcons.Add("Palm Tree", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandpalmlog.png")));
            mapIcons.Add("Wild Parsnip", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandparsnipseeds.png")));
            mapIcons.Add("Wild Popoto", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandpopotoseeds.png")));
            mapIcons.Add("Lightly Gnawed Pumpkin", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandpumpkinseeds.png")));
            mapIcons.Add("Quartz Formation", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandquartz.png")));
            mapIcons.Add("Crystal-banded Rock", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandrocksalt.png")));
            mapIcons.Add("Mahogany Tree", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandsap.png")));
            mapIcons.Add("Seaweed Tangle", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandsquid.png")));
            mapIcons.Add("Sugarcane", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandsugarcane.png")));
            mapIcons.Add("Submerged Sand", this.PluginInterface.UiBuilder.LoadImage(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "map_icons\\islandtinsand.png")));

            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new MainWindow(this, mapImage, mapIcons));

            this.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "IT'S A MAP"
            });

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
            this.CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            WindowSystem.GetWindow("Isle Map").IsOpen = true;
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            WindowSystem.GetWindow("Filter Nodes").IsOpen = true;
        }
    }
}
