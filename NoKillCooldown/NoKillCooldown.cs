using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;

namespace NoKillCooldown;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public partial class NoKillCooldown : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public static BepInEx.Logging.ManualLogSource log;

    public override void Load()
    {
        log = Log;
        log.LogMessage("No Kill Cooldown Mod has loaded");
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