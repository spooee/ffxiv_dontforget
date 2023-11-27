using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin.Services;
using ECommons.DalamudServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dontforgetthecard
{
    internal static class Events
    {
        private static uint? jobID;

        public static void Init()
        {
            Service.Framework.Update += UpdateEvents;
        }

        public static void Disable()
        {
            Service.Framework.Update -= UpdateEvents;
        }

        private static void UpdateEvents(IFramework framework)
        {
            if (Service.ClientState.LocalPlayer is null) return;
            JobID = Service.ClientState.LocalPlayer.ClassJob.Id;
        }

        public static uint? JobID
        {
            get => jobID;
            set
            {
                if (value != null && jobID != value)
                {
                    jobID = value;
                    Service.Log.Debug($"Job changed to {value}");
                    OnJobChanged?.Invoke(value);
                }
            }
        }

        public delegate void OnJobChangeDelegate(uint? jobId);
        public static event OnJobChangeDelegate? OnJobChanged;

    }
}
