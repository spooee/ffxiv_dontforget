using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using ImGuiNET;

namespace dontforgetthecard;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;
    public ConfigWindow(Plugin plugin) : base(
        "Don't Forget the Card",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(260, 170);
        this.SizeCondition = ImGuiCond.Always;
        this.Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        var astrologianConfig = this.Configuration.Astrologian;
        var scholarConfig = this.Configuration.Scholar;
        var summonerConfig = this.Configuration.Summoner;
        var peletonConfig = this.Configuration.Peleton;

        ImGui.TextWrapped("Enable for jobs you want to not forget!");
        ImGui.Spacing();

        if (ImGui.Checkbox("Astrologian - Draw Card", ref astrologianConfig))
        {
            this.Configuration.Astrologian = astrologianConfig;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Scholar - Summon Fairy", ref scholarConfig))
        {
            this.Configuration.Scholar = scholarConfig;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Summoner - Summon Carbuncle", ref  summonerConfig))
        {
            this.Configuration.Summoner = summonerConfig;
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Phys Ranged - Auto Peleton", ref peletonConfig))
        {
            this.Configuration.Peleton = peletonConfig;
            this.Configuration.Save();
        }
    }
}
