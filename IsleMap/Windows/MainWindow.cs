using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Dalamud.Interface.Windowing;
using Dalamud.Game;
using ImGuiNET;
using ImGuiScene;
using IsleMap.Properties;
using System.Linq;
using Lumina.Excel.GeneratedSheets;
using System.Drawing;
using Dalamud.Game.ClientState.Objects.Types;

namespace IsleMap.Windows;

public class MainWindow : Window, IDisposable
{
    private TextureWrap MapImage;
    private Plugin Plugin;
    private Dictionary<string, TextureWrap> MapIcons;

    float wheel_counter;
    float opacity = 1.0f;
    List<Material> nodeList = new List<Material>();
    List<Filter> filterList = new List<Filter>();


    public MainWindow(Plugin plugin, TextureWrap mapImage, Dictionary<string, TextureWrap> mapIcons) : base(
        "Isle Map", ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.NoBackground 
        | ImGuiWindowFlags.AlwaysHorizontalScrollbar | ImGuiWindowFlags.AlwaysVerticalScrollbar)
    {
        
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(512, 512),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.MapImage = mapImage;
        this.Plugin = plugin;
        this.MapIcons = mapIcons;
        wheel_counter = 1;
        if (wheel_counter < 1)
        {
            wheel_counter = 1;
        }

        //nodeList
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(32.420975), convertedY(19.795017)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(31.42768), convertedY(19.38905)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(33.316048), convertedY(16.809177)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.74905), convertedY(11.4007435)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(29.83548), convertedY(13.363553)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(29.760698), convertedY(22.34534)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(29.39404), convertedY(23.08402)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.82862), convertedY(23.51358)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.64899), convertedY(24.279799)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.378258), convertedY(24.226677)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.382921), convertedY(24.77903)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.630207), convertedY(25.462193)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(29.82119), convertedY(26.499388)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.752468), convertedY(26.425667)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(29.223448), convertedY(29.732544)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(29.593346), convertedY(30.138226)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(28.179865), convertedY(30.111397)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(27.624153), convertedY(30.019453)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(25.340492), convertedY(29.377768)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(17.907888), convertedY(22.647064)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(16.634144), convertedY(22.09366)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(11.69404), convertedY(21.169155)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(14.388382), convertedY(15.927614)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(14.688288), convertedY(13.78709)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(15.8701515), convertedY(13.53276)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(16.681122), convertedY(12.073337)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(18.473484), convertedY(13.7941675)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(19.315262), convertedY(14.373792)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(20.023918), convertedY(11.135628)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(20.179668), convertedY(11.207525)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(20.311083), convertedY(12.359959)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(19.76122), convertedY(18.10144)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(20.36154), convertedY(21.655096)));
        nodeList.Add(new Material("Agave Plant", "Island Hemp", "Islewort", convertedX(25.908964), convertedY(21.156487)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(24.257053), convertedY(10.1026745)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(28.376347), convertedY(10.034118)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(31.236397), convertedY(12.151804)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(28.970798), convertedY(10.337778)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(30.224667), convertedY(11.384678)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(31.540134), convertedY(27.88003)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(31.417343), convertedY(28.300406)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(31.765514), convertedY(28.439774)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(29.198967), convertedY(27.701271)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(30.80312), convertedY(28.660822)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(29.993149), convertedY(28.693554)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(30.480125), convertedY(29.427902)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(31.253138), convertedY(29.325758)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(16.95377), convertedY(16.48325)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(16.30022), convertedY(15.6197195)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(17.599087), convertedY(16.459124)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(16.497267), convertedY(15.3070755)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(17.356165), convertedY(15.661768)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(17.413067), convertedY(15.474292)));
        nodeList.Add(new Material("Bluish Rock", "Island Copper Ore", "Island Stone", convertedX(17.487152), convertedY(15.265174)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(32.47278), convertedY(23.741043)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(32.893753), convertedY(24.501312)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(32.068565), convertedY(25.089123)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(32.51969), convertedY(25.020588)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(32.21728), convertedY(25.112663)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(31.987522), convertedY(25.922218)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(32.28434), convertedY(26.318071)));
        nodeList.Add(new Material("Coral Formation", "Island Jellyfish", "Island Coral", convertedX(32.51546), convertedY(26.490597)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(30.96848), convertedY(21.40945)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(33.74472), convertedY(18.884708)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(26.941196), convertedY(12.80251)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(26.859533), convertedY(21.21324)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(27.675434), convertedY(29.174826)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(27.73411), convertedY(29.27372)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(27.424782), convertedY(29.098898)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(27.499577), convertedY(29.183037)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(27.579851), convertedY(29.24414)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(27.252007), convertedY(29.089273)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(27.381706), convertedY(29.19342)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(23.993374), convertedY(26.11895)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(16.830967), convertedY(22.414152)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.576635), convertedY(22.981588)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.403984), convertedY(22.963484)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.7732115), convertedY(22.488668)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.348398), convertedY(22.650581)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.963014), convertedY(22.02304)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(12.922302), convertedY(22.285992)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.603248), convertedY(21.971594)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(12.793704), convertedY(22.109327)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.0730095), convertedY(21.81455)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(12.697406), convertedY(21.794281)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(12.455855), convertedY(20.0329)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(12.559828), convertedY(18.195074)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.923519), convertedY(17.534096)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(16.63242), convertedY(17.82057)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(13.670666), convertedY(13.898638)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(20.69442), convertedY(10.21809)));
        nodeList.Add(new Material("Cotton Plant", "Island Cotton Boll", "Islewort", convertedX(19.082338), convertedY(17.034874)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(31.604725), convertedY(21.024231)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(31.589407), convertedY(21.117006)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(31.658478), convertedY(21.136826)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(31.536), convertedY(21.066225)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(31.643513), convertedY(20.947857)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(31.652359), convertedY(20.998463)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(19.208782), convertedY(12.227409)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(19.293255), convertedY(12.25466)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(19.324234), convertedY(12.069196)));
        nodeList.Add(new Material("Crystal-banded Rock", "Island Rock Salt", "Island Stone", convertedX(19.351768), convertedY(12.160861)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(23.681208), convertedY(12.673512)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(28.58189), convertedY(13.325828)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(26.028652), convertedY(22.961145)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(23.500217), convertedY(26.976833)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(23.741253), convertedY(25.43472)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(23.470844), convertedY(24.578783)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(21.466898), convertedY(25.390324)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(20.301815), convertedY(26.89597)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(20.77978), convertedY(25.324293)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(19.49313), convertedY(26.78842)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(20.300137), convertedY(24.949738)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(19.618471), convertedY(24.602259)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(18.062542), convertedY(26.771461)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(18.881811), convertedY(12.875164)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(20.19567), convertedY(11.8606825)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(20.043766), convertedY(15.748288)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(20.436321), convertedY(19.93593)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(20.23339), convertedY(20.521406)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(21.972664), convertedY(20.991552)));
        nodeList.Add(new Material("Island Apple Tree", "Island Apple", "Island Vine", convertedX(23.470663), convertedY(21.575813)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(15.24805), convertedY(10.065169)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(15.421745), convertedY(9.747512)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(17.723658), convertedY(10.358327)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(17.93456), convertedY(10.618173)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(18.379793), convertedY(10.950118)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(18.432741), convertedY(9.892776)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(18.91508), convertedY(10.520188)));
        nodeList.Add(new Material("Large Shell", "Island Clam", "Islefish", convertedX(18.782623), convertedY(10.129936)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(20.036608), convertedY(26.122734)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(19.785725), convertedY(26.222013)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(19.685526), convertedY(26.106731)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(19.508892), convertedY(26.275127)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(19.438646), convertedY(26.304323)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(18.783287), convertedY(26.346651)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(18.714127), convertedY(26.421463)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(18.699345), convertedY(26.36731)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(18.602566), convertedY(26.39741)));
        nodeList.Add(new Material("Lightly Gnawed Pumpkin", "Island Pumpkin Seed", "None", convertedX(18.521349), convertedY(26.350014)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(24.184599), convertedY(11.71436)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(32.543793), convertedY(15.757332)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(27.94548), convertedY(12.854627)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(30.715805), convertedY(12.973772)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(28.591911), convertedY(20.986197)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(27.764301), convertedY(28.084852)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(26.053503), convertedY(27.582428)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(22.428976), convertedY(25.032602)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(22.01449), convertedY(24.364485)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(21.216429), convertedY(24.81348)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(20.529964), convertedY(24.560877)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(19.468502), convertedY(23.545893)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(19.086966), convertedY(22.22425)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(14.507272), convertedY(20.763237)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(18.50888), convertedY(21.31002)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(17.012796), convertedY(20.464375)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(15.904694), convertedY(18.617668)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(19.128435), convertedY(21.273481)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(13.2287655), convertedY(16.31666)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(19.040022), convertedY(20.21815)));
        nodeList.Add(new Material("Mahogany Tree", "Island Sap", "Island Log", convertedX(22.617832), convertedY(22.47993)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(29.979855), convertedY(22.197914)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(29.418407), convertedY(23.854002)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(29.366676), convertedY(25.453838)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(28.732315), convertedY(27.497358)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(24.649734), convertedY(26.332539)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(22.913847), convertedY(26.044397)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(22.863825), convertedY(26.078758)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(22.791119), convertedY(26.07224)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(22.76442), convertedY(25.97637)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(22.698414), convertedY(26.026573)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(22.617615), convertedY(26.009693)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(22.435978), convertedY(25.981201)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(18.059069), convertedY(18.623722)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(18.092203), convertedY(18.651613)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(14.904843), convertedY(13.471855)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(15.204151), convertedY(13.42235)));
        nodeList.Add(new Material("Mound of Dirt", "Island Clay", "Island Sand", convertedX(19.16933), convertedY(14.850642)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(31.713701), convertedY(20.338161)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(33.601395), convertedY(18.576292)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(29.280853), convertedY(13.000372)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(29.549435), convertedY(22.187872)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(28.832592), convertedY(23.789366)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(28.800674), convertedY(25.430683)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(27.9136), convertedY(25.213408)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(29.093945), convertedY(26.336918)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(29.892473), convertedY(27.68564)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(27.60133), convertedY(30.54866)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(19.493908), convertedY(25.664196)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(18.162586), convertedY(26.284588)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(18.340498), convertedY(25.649382)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(19.083168), convertedY(23.490063)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(17.439846), convertedY(23.664888)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(18.9532), convertedY(22.787518)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(18.095009), convertedY(22.853695)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(17.562061), convertedY(22.954842)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(17.732996), convertedY(22.242971)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(17.950077), convertedY(22.091553)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(14.210095), convertedY(13.320248)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(15.367665), convertedY(13.210727)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(16.121464), convertedY(12.96919)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(17.243324), convertedY(13.893055)));
        nodeList.Add(new Material("Palm Tree", "Island Palm Log", "Island Palm Leaf", convertedX(16.993183), convertedY(13.174678)));
        nodeList.Add(new Material("Partially Consumed Cabbage", "Island Cabbage Seed", "None", convertedX(15.697242), convertedY(19.902317)));
        nodeList.Add(new Material("Partially Consumed Cabbage", "Island Cabbage Seed", "None", convertedX(17.554075), convertedY(20.468807)));
        nodeList.Add(new Material("Partially Consumed Cabbage", "Island Cabbage Seed", "None", convertedX(17.486746), convertedY(20.345474)));
        nodeList.Add(new Material("Partially Consumed Cabbage", "Island Cabbage Seed", "None", convertedX(17.49256), convertedY(20.23637)));
        nodeList.Add(new Material("Partially Consumed Cabbage", "Island Cabbage Seed", "None", convertedX(17.463686), convertedY(20.175713)));
        nodeList.Add(new Material("Partially Consumed Cabbage", "Island Cabbage Seed", "None", convertedX(17.50224), convertedY(20.081726)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(27.314632), convertedY(19.372307)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(26.685537), convertedY(19.111485)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(29.27129), convertedY(18.514576)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(27.95753), convertedY(10.332576)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(22.23514), convertedY(16.373764)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(22.281372), convertedY(16.381624)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(22.264736), convertedY(16.52521)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(22.303879), convertedY(16.495722)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(24.836054), convertedY(16.095242)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(24.976759), convertedY(16.137486)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(25.110718), convertedY(16.0613)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(25.067947), convertedY(16.243093)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(25.164024), convertedY(16.144562)));
        nodeList.Add(new Material("Quartz Formation", "Island Quartz", "Island Stone", convertedX(25.08615), convertedY(16.3206)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(25.986477), convertedY(19.724398)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(26.028065), convertedY(19.695105)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(25.96991), convertedY(19.653767)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(26.659996), convertedY(20.026491)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(24.246021), convertedY(12.445883)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(27.420708), convertedY(19.6747)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(26.422672), convertedY(19.338581)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(28.867767), convertedY(19.596876)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(27.176853), convertedY(19.298292)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(27.010761), convertedY(19.067488)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(29.43674), convertedY(18.850567)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(29.133556), convertedY(18.721756)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(28.923803), convertedY(9.974417)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(29.08117), convertedY(13.627902)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(29.648571), convertedY(10.971452)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(25.02556), convertedY(16.063278)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(24.936504), convertedY(16.301474)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(25.202127), convertedY(16.249931)));
        nodeList.Add(new Material("Rough Black Rock", "Island Iron Ore", "Island Stone", convertedX(24.460636), convertedY(19.550283)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(32.957924), convertedY(23.494337)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(32.125126), convertedY(23.701002)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(32.210163), convertedY(24.165077)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(33.1545), convertedY(24.237116)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(32.63639), convertedY(25.447462)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(32.64386), convertedY(25.759012)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(32.803658), convertedY(26.733421)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(32.3562), convertedY(26.927353)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(13.836242), convertedY(10.247353)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(15.041814), convertedY(10.58865)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(15.348873), convertedY(9.830589)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(17.493261), convertedY(10.244418)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(17.820486), convertedY(9.89928)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(18.906818), convertedY(10.660495)));
        nodeList.Add(new Material("Seaweed Tangle", "Island Squid", "Island Laver", convertedX(19.041662), convertedY(10.042612)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(24.27627), convertedY(11.196044)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(29.075562), convertedY(20.308079)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(26.297125), convertedY(10.91965)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(27.186714), convertedY(11.245949)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(28.79723), convertedY(21.262016)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(28.815342), convertedY(21.456)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(29.074516), convertedY(21.940018)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(28.739506), convertedY(22.617739)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(27.694412), convertedY(22.87011)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(22.386303), convertedY(25.93094)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(17.353138), convertedY(24.326422)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(17.556746), convertedY(23.911398)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(16.827545), convertedY(24.234255)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(18.443974), convertedY(22.972195)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(18.392378), convertedY(22.76176)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(18.488571), convertedY(22.699162)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(18.300365), convertedY(22.70492)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(18.390116), convertedY(22.538286)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(18.373272), convertedY(22.433352)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(18.183752), convertedY(22.149714)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(14.678357), convertedY(15.113348)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(14.977457), convertedY(15.200222)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(15.045145), convertedY(14.6339035)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(15.449723), convertedY(14.7156515)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(14.320184), convertedY(14.331464)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(13.978817), convertedY(13.621088)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(17.261202), convertedY(15.2289915)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(21.61824), convertedY(11.250396)));
        nodeList.Add(new Material("Smooth White Rock", "Island Limestone", "Island Stone", convertedX(21.991076), convertedY(11.346801)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(28.85984), convertedY(19.513685)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(26.882404), convertedY(20.036766)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(26.330954), convertedY(10.164189)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(28.884432), convertedY(18.876556)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(27.443832), convertedY(10.0665455)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(27.548561), convertedY(19.56248)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(32.15915), convertedY(12.642143)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(31.855236), convertedY(11.304342)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(31.394653), convertedY(11.14143)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(29.623508), convertedY(18.636122)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(31.085936), convertedY(11.940025)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(30.472317), convertedY(11.328784)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(29.162842), convertedY(9.625658)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(29.902084), convertedY(10.887647)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(29.41726), convertedY(10.455744)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(24.915413), convertedY(16.231209)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(25.141962), convertedY(16.282795)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(23.795443), convertedY(19.775898)));
        nodeList.Add(new Material("Speckled Rock", "Island Leucogranite", "Island Stone", convertedX(24.246847), convertedY(19.63242)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(27.323486), convertedY(26.714308)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(25.817577), convertedY(26.691593)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(23.257715), convertedY(25.946632)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(21.728806), convertedY(25.83354)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(18.615744), convertedY(22.417269)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(18.15954), convertedY(21.943796)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(18.085686), convertedY(21.739658)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(18.143408), convertedY(21.181955)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(17.952131), convertedY(20.909372)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(18.108292), convertedY(20.433239)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(18.047024), convertedY(20.242044)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(17.54575), convertedY(18.664223)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(17.815222), convertedY(17.794044)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(17.654257), convertedY(16.949816)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(17.900915), convertedY(15.638916)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(17.757278), convertedY(14.395166)));
        nodeList.Add(new Material("Submerged Sand", "Island Tinsand", "Island Sand", convertedX(18.000513), convertedY(13.216784)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(25.80835), convertedY(26.945179)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(23.217236), convertedY(25.678041)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(21.275597), convertedY(25.948475)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(20.376984), convertedY(25.798872)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(17.60459), convertedY(24.331718)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.334627), convertedY(23.240925)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(17.270641), convertedY(17.902716)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(17.3864), convertedY(17.813992)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(17.269913), convertedY(17.600475)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(17.373672), convertedY(17.455996)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.07736), convertedY(17.953285)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.136774), convertedY(17.869102)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.031984), convertedY(17.620975)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.123035), convertedY(17.339193)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.2676), convertedY(17.453232)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.46773), convertedY(16.937908)));
        nodeList.Add(new Material("Sugarcane", "Island Sugarcane", "Island Vine", convertedX(18.496412), convertedY(17.041008)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(25.145105), convertedY(11.306292)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(30.021303), convertedY(19.13332)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(26.67274), convertedY(11.978206)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(32.36554), convertedY(18.200596)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(31.390755), convertedY(15.156366)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(30.26664), convertedY(14.779562)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(29.27926), convertedY(11.1214695)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(27.480442), convertedY(22.393642)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(27.982655), convertedY(28.294273)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(25.690178), convertedY(30.56583)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(23.870855), convertedY(25.206337)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(23.07673), convertedY(26.504887)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(22.86085), convertedY(25.351105)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(21.942467), convertedY(26.564045)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(21.400082), convertedY(26.918615)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(21.898495), convertedY(25.446606)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(21.298641), convertedY(25.201496)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(20.603384), convertedY(25.942223)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(24.568033), convertedY(23.066195)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(19.810507), convertedY(25.757814)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(19.022854), convertedY(27.071354)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(13.211996), convertedY(22.488567)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(12.796021), convertedY(22.283615)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(16.498539), convertedY(21.544794)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(13.741842), convertedY(20.059135)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(13.048044), convertedY(17.289808)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(12.490505), convertedY(15.865114)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(16.697731), convertedY(16.681461)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(20.458118), convertedY(13.881892)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(21.991428), convertedY(12.103678)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(23.48742), convertedY(10.664294)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(20.48602), convertedY(16.651768)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(21.320757), convertedY(19.111303)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(20.148218), convertedY(19.39376)));
        nodeList.Add(new Material("Tualong Tree", "Island Branch", "Island Log", convertedX(24.511322), convertedY(20.79627)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(28.051083), convertedY(22.996832)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(27.772213), convertedY(23.356838)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(27.508795), convertedY(23.424034)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(26.767159), convertedY(23.674465)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(26.970257), convertedY(24.429577)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(26.416351), convertedY(23.605864)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(26.374409), convertedY(24.385921)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(30.3028), convertedY(28.604733)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(29.09436), convertedY(28.76867)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(26.20573), convertedY(30.157608)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(26.403904), convertedY(30.714518)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(22.913616), convertedY(23.636015)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(21.715225), convertedY(23.395174)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(20.162853), convertedY(23.293955)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(14.785968), convertedY(20.085506)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(14.702109), convertedY(17.427326)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(12.905826), convertedY(15.71718)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(13.410213), convertedY(15.54016)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(12.5376835), convertedY(15.363994)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(14.103322), convertedY(15.390618)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(12.85858), convertedY(15.046327)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(13.049804), convertedY(14.82828)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(13.233436), convertedY(13.9162655)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(18.76719), convertedY(18.694983)));
        nodeList.Add(new Material("Wild Parsnip", "Island Parsnip Seeds", "Islewort", convertedX(22.562677), convertedY(21.998589)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(25.905796), convertedY(25.280811)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(25.609911), convertedY(24.65115)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(25.834753), convertedY(26.016317)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(25.586481), convertedY(25.234573)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(24.950752), convertedY(26.934114)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(24.93389), convertedY(26.217222)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(24.587145), convertedY(25.86779)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(24.369719), convertedY(26.263618)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(23.183044), convertedY(23.908123)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(21.797056), convertedY(22.89005)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(16.592274), convertedY(23.15165)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(15.047945), convertedY(20.5128)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(10.652938), convertedY(20.977987)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(12.939772), convertedY(13.086479)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(13.163168), convertedY(12.604318)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(12.693654), convertedY(12.390742)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(13.034942), convertedY(12.371674)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(13.234734), convertedY(12.159843)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(12.904026), convertedY(11.896884)));
        nodeList.Add(new Material("Wild Popoto", "Island Popoto Set", "Islewort", convertedX(13.226692), convertedY(11.51851)));

    }
    public int convertedX(double inputX)
    {
        int originFFX = 1;
        int spacing = 50;
        int conx = (int)((inputX - originFFX) * spacing);

        return conx;
    }
    private int convertedY(double inputY)
    {
        int originFFY = 1;
        int spacing = 50;
        int cony = (int)((inputY - originFFY) * spacing);

        return cony;
    }
    public void Dispose()
    {
        this.MapImage.Dispose();
        foreach(var item in MapIcons)
        {
            item.Value.Dispose();
        }
    }
    public override void Draw()
    {
        if (ImGui.IsKeyDown(ImGuiKey.LeftCtrl))
        {
           wheel_counter += (ImGui.GetIO().MouseWheel/20);
        }
        
        //filter list
        filterList.Add(new Filter(0, "Select/Deselect All: All materials/No materials", this.Plugin.Configuration.node1, false));
        filterList.Add(new Filter(1, "Island Apple Tree: Island Apple and Island Vine", this.Plugin.Configuration.node2, false));
        filterList.Add(new Filter(2, "Tualong Tree: Island Branch and Island Log", this.Plugin.Configuration.node3, false));
        filterList.Add(new Filter(3, "Partially Consumed Cabbage: Island Cabbage Seed", this.Plugin.Configuration.node4, false));
        filterList.Add(new Filter(4, "Large Shell: Island Clam and Islefish", this.Plugin.Configuration.node5, false));
        filterList.Add(new Filter(5, "Mound of Dirt: Island Clay and Island Sand", this.Plugin.Configuration.node6, false));
        filterList.Add(new Filter(6, "Bluish Rock: Island Copper Ore and Island Stone", this.Plugin.Configuration.node7, false));
        filterList.Add(new Filter(7, "Cotton Plant: Island Cotton Boll and Islewort", this.Plugin.Configuration.node8, false));
        filterList.Add(new Filter(8, "Agave Plant: Island Hemp and Islewort", this.Plugin.Configuration.node9, false));
        filterList.Add(new Filter(9, "Rough Black Rock: Island Iron Ore and Island Stone", this.Plugin.Configuration.node10, false));
        filterList.Add(new Filter(10, "Coral Formation: Island Jellyfish and Island Coral", this.Plugin.Configuration.node11, false));
        filterList.Add(new Filter(11, "Speckled Rock: Island Leucogranite and Island Stone", this.Plugin.Configuration.node12, false));
        filterList.Add(new Filter(12, "Smooth White Rock: Island Limestone and Island Stone", this.Plugin.Configuration.node13, false));
        filterList.Add(new Filter(13, "Palm Tree: Island Palm Log and Island Palm Leaf", this.Plugin.Configuration.node14, false));
        filterList.Add(new Filter(14, "Wild Parsnip: Island Parsnip Seeds and Islewort", this.Plugin.Configuration.node15, false));
        filterList.Add(new Filter(15, "Wild Popoto: Island Popoto Set and Islewort", this.Plugin.Configuration.node16, false));
        filterList.Add(new Filter(16, "Lightly Gnawed Pumpkin: Island Pumpkin Seed", this.Plugin.Configuration.node17, false));
        filterList.Add(new Filter(17, "Quartz Formation: Island Quartz and Island Stone", this.Plugin.Configuration.node18, false));
        filterList.Add(new Filter(18, "Crystal-banded Rock: Island Rock Salt and Island Stone", this.Plugin.Configuration.node19, false));
        filterList.Add(new Filter(19, "Mahogany Tree: Island Sap and Island Log", this.Plugin.Configuration.node20, false));
        filterList.Add(new Filter(20, "Seaweed Tangle: Island Squid and Island Laver", this.Plugin.Configuration.node21, false));
        filterList.Add(new Filter(21, "Sugarcane: Island Sugarcane and Island Vine", this.Plugin.Configuration.node22, false));
        filterList.Add(new Filter(22, "Submerged Sand: Island Tinsand and Island Sand", this.Plugin.Configuration.node23, false));

        if (ImGui.Button("Filter Nodes"))
        {
            this.Plugin.DrawConfigUI();
        }
        ImGui.SameLine();
        ImGui.Text("Map Opacity: ");
        ImGui.SameLine();
        if (ImGui.Button("-"))
        {
             opacity -= 0.1f;
        }
        ImGui.SameLine();
        if(ImGui.Button("+"))
        {
            opacity += 0.1f;
        }
        ImGui.SameLine();
        if (ImGui.Button("Reset Opacity"))
        {
            opacity = 1.0f;
        }
        ImGui.SameLine();
        if (ImGui.Button("Reset Zoom"))
        {
            wheel_counter = 1;
        }

        ImGui.Image(this.MapImage.ImGuiHandle, new Vector2((this.MapImage.Width / 2) * wheel_counter, (this.MapImage.Height / 2) * wheel_counter), 
            new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector4(1.0f,1.0f,1.0f,opacity));
      
        for (int i = 0; i < filterList.Count; i++)
        {
            if (filterList.ElementAt(i).FilterState != filterList.ElementAt(i).IsNodeVisible)
            {
                foreach (var item in nodeList)
                {
                    filterList.ElementAt(i).FilterState = filterList.ElementAt(i).IsNodeVisible;
                    if (filterList.ElementAt(i).IsNodeVisible && filterList.ElementAt(i).NodeName.Contains(item.NodeName))
                    {
                        Vector2 mapcon_size = new Vector2((this.MapIcons.FirstOrDefault(icon => icon.Key == item.NodeName).Value.Width / 7) * wheel_counter,
                            (this.MapIcons.FirstOrDefault(icon => icon.Key == item.NodeName).Value.Height / 7) * wheel_counter);
                        Vector2 pos = new Vector2((item.X/2) * wheel_counter, (item.Y/2) * wheel_counter);                        
                        Vector2 windowPos = new Vector2(ImGui.GetWindowPos().X + 10, ImGui.GetWindowPos().Y + 60);
                        TextureWrap mapcon = this.MapIcons.FirstOrDefault(icon => icon.Key == item.NodeName).Value;

                        ImGui.GetWindowDrawList().AddImage(mapcon.ImGuiHandle, new Vector2(pos.X + windowPos.X - ImGui.GetScrollX(), pos.Y + windowPos.Y - ImGui.GetScrollY()),
                            new Vector2(pos.X + windowPos.X + mapcon_size.X - ImGui.GetScrollX(), pos.Y + windowPos.Y + mapcon_size.Y - ImGui.GetScrollY()));
                    }               
                }
            }
        }   
            
        Vector2 screenpos = ImGui.GetCursorScreenPos();
        string mouse_x = Math.Round((((((ImGui.GetIO().MousePos.X - screenpos.X) / wheel_counter) * 4) / 50) + 1 ),1).ToString();
        string mouse_y = Math.Round((((((ImGui.GetIO().MousePos.Y - screenpos.Y + 31) / wheel_counter) * 4) / 50) + 1), 1).ToString();
        ImGui.Spacing();
        //ImGui.Text("Island Sanctuary Map " + wheel_counter + " " + mouse_x + " " + mouse_y 
        //    + " screenposX: " + screenpos.X + " mouseposX: " + ImGui.GetIO().MousePos.X + " screenposY: " 
        //    + screenpos.Y + " mouseposY: " + ImGui.GetIO().MousePos.Y);
        ImGui.Indent(55);
        ImGui.Unindent(55);
    }
}
