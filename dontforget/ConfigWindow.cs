using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace dontforget;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;
    public ConfigWindow(Plugin plugin) : base(
        "Don't Forget",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(270, 190);
        this.SizeCondition = ImGuiCond.Always;
        this.Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        var astrologianConfig = this.Configuration.Astrologian;
        var scholarConfig = this.Configuration.Scholar;
        var summonerConfig = this.Configuration.Summoner;
        var pelotonConfig = this.Configuration.Peloton;

        ImGui.TextWrapped("Enable for jobs you don't want to forget");
        ImGui.Spacing();

        if (ImGui.Checkbox("Astrologian - Draw Card", ref astrologianConfig))
        {
            this.Configuration.Astrologian = astrologianConfig;
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Phys Ranged - Auto Peloton", ref pelotonConfig))
        {
            this.Configuration.Peloton = pelotonConfig;
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
    }
}
