using Dalamud.IoC;
using Dalamud.Plugin.Services;

namespace dontforget
{
    internal class Service
    {
        [PluginService] public static IClientState ClientState { get; private set; } = null!;
        [PluginService] public static ICondition Condition { get; private set; } = null!;
        [PluginService] public static IFramework Framework { get; private set; } = null!;
        [PluginService] public static IDutyState DutyState { get; private set; } = null!;
    }
}
