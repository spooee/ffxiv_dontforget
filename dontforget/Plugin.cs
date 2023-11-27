using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using Dalamud.Game.ClientState.Conditions;
using System.Linq;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace dontforgetthecard
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Don't Forget";
        private const string CommandName = "/dontforget";
        private DalamudPluginInterface PluginInterface { get; init; }
        private ICommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("Don't Forget");
        private ConfigWindow ConfigWindow { get; init; }
        private unsafe static ActionManager* AM;
        private uint drawCard = 3590;
        private uint summonFairy = 17215;
        private uint summonCarbuncle = 25798;
        private uint peleton = 7557;

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] ICommandManager commandManager)
        {
            this.PluginInterface = pluginInterface;
            this.CommandManager = commandManager;
            this.PluginInterface.Create<Service>(this);
            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            ConfigWindow = new ConfigWindow(this);
            WindowSystem.AddWindow(ConfigWindow);

            this.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Open the config window"
            });
            unsafe
            {
                LoadUnsafe();
            }
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
            this.PluginInterface.UiBuilder.Draw += DrawUI;
            Service.DutyState.DutyStarted += onDuty;
            Service.DutyState.DutyRecommenced += onDuty;
            Service.Framework.Update += onFrameworkUpdate;
        }

        private unsafe void LoadUnsafe()
        {
            AM = ActionManager.Instance();
        }

        private unsafe void onDuty(object? sender, ushort t)
        {
            bool actionPerformed = true;
            uint actionID = drawCard;

            if (Service.ClientState == null || Service.ClientState.LocalPlayer == null || !Service.ClientState.IsLoggedIn) return;


            var classJobID = Service.ClientState.LocalPlayer.ClassJob.Id;

            if (classJobID == 33 && this.Configuration.Astrologian)
            {

                actionID = drawCard;
                actionPerformed = AM->UseAction(ActionType.Action, actionID);
            }

            else if (classJobID == 28 && this.Configuration.Scholar)
            {
                actionID = summonFairy;
                actionPerformed = AM->UseAction(ActionType.Action, actionID);
            }

            else if (classJobID == 27 && this.Configuration.Summoner)
            {
                actionID = summonCarbuncle;
                actionPerformed = AM->UseAction(ActionType.Action, actionID);
            }
        }

        private unsafe void onFrameworkUpdate(IFramework framework)
        {
            if (Service.ClientState == null || Service.ClientState.LocalPlayer == null || !Service.ClientState.IsLoggedIn || Service.Condition[ConditionFlag.InCombat]) return;

            var isPeletonReady = AM->GetActionStatus(ActionType.Action, peleton) == 0;
            var hasPeletonBuff = Service.ClientState.LocalPlayer.StatusList.Any(x => x.StatusId == 1199 || x.StatusId == 50);

            if (this.Configuration.Peleton && isPeletonReady && !hasPeletonBuff && AgentMap.Instance()->IsPlayerMoving == 1)
            {
                AM->UseAction(ActionType.Action, peleton);
            }
        }

        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
            this.CommandManager.RemoveHandler(CommandName);
            Service.DutyState.DutyStarted -= onDuty;
            Service.DutyState.DutyRecommenced -= onDuty;
            Service.Framework.Update -= onFrameworkUpdate;
        }

        private void OnCommand(string command, string args)
        {
            ConfigWindow.IsOpen = true;
        }

        public void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            this.ConfigWindow.IsOpen = true;
        }

    }
}
