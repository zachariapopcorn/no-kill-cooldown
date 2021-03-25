using BepInEx;
using BepInEx.Configuration;
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

        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
        public static class KillCooldownAfterKillPatch
        {
            public static void Postfix(PlayerControl __instance)
            {
                __instance.SetKillTimer(0);
            }
        }
        [HarmonyPatch(typeof(ExileController), nameof(ExileController.Method_24))]
        public static class MeetingPatch
        {
            public static void Postfix()
            {
                PlayerControl.LocalPlayer.SetKillTimer(0);
            }
        }
    }
}