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
        [HarmonyPatch(typeof(AirshipExileController._WrapUpAndSpawn_d__11), nameof(AirshipExileController._WrapUpAndSpawn_d__11.MoveNext))]
        public static class AirShipMeetingPatch
        {
            public static void Postfix()
            {
                var players = PlayerControl.AllPlayerControls;
                for(int i = 0; i < players.Count; i++)
                {
                    var player = players[i];
                    var playerInfo = player.Data;
                    if(playerInfo.IsImpostor)
                    {
                        player.SetKillTimer(0);
                    }
                }
            }
        }
        [HarmonyPatch(typeof(ExileController), nameof(ExileController.GALOAPAFIMJ))]
        public static class MeetingPatch
        {
            public static void Postfix()
            {
                var players = PlayerControl.AllPlayerControls;
                for(int i = 0; i < players.Count; i++)
                {
                    var player = players[i];
                    var playerInfo = player.Data;
                    if(playerInfo.IsImpostor)
                    {
                        player.SetKillTimer(0);
                    }
                }
            }
        }
    }
}