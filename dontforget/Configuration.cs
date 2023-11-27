using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace dontforgetthecard
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        public bool Astrologian { get; set; } = true;
        public bool Scholar { get; set; } = true;
        public bool Summoner { get; set; } = true;
        public bool Peleton { get; set; } = true;

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
