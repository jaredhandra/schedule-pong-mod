using MelonLoader;
using HarmonyLib;
using ScheduleOne.Product;
using ScheduleOne.PlayerScripts;
using UnityEngine;


[assembly: MelonInfo(typeof(PongMod.PongMod), PongMod.BuildInfo.Name, PongMod.BuildInfo.Version, PongMod.BuildInfo.Author, PongMod.BuildInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace PongMod
{
    public static class BuildInfo
    {
        public const string Name = "Ping Pong App";
        public const string Description = "Play some pong";
        public const string Author = "sleepingsuns";
        public const string Company = null;
        public const string Version = "0.1";
        public const string DownloadLink = null;
    }

    public class PongMod : MelonMod
    {
        [HarmonyPatch(typeof(Player), "ConsumeProduct")]
        public static class Player_ConsumeProduct_Patch
        {
            public static bool Prefix(Player __instance, ProductItemInstance product)
            {
                MelonLogger.Msg("Product is being consumed");
                return true;
            }
        }

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Ping Pong Mod Loaded");
            HarmonyInstance.PatchAll();
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                MelonLogger.Msg("Pong!");
            }
        }
    }
}
