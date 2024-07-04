using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace dontforget
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;
        public bool Scholar { get; set; } = true;
        public bool Summoner { get; set; } = true;
        public bool Peloton { get; set; } = true;

        [NonSerialized]
        private IDalamudPluginInterface? PluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            this.PluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
