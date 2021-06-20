using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;

namespace NoKillCooldown
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class NoKillCooldown : BasePlugin
    {
        public const string Id = "DisableKillCooldown";
        public static BepInEx.Logging.ManualLogSource log;

        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            log = Log;
            log.LogMessage("No Kill Cooldown has loaded");
            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetKillTimer))]
        public static class KillTimerPatch
        {
            public static void Prefix([HarmonyArgument(0)] ref float time)
            {
                time = 0f;
            }
        }

    }
}