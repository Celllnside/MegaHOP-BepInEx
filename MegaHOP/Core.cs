using Assets.Scripts.Actors.Player;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;


namespace MegaHOP
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Core : BasePlugin
    {
        public const string PluginGuid = "com.cellinside.MegaHopBepInEx";
        public const string PluginName = "megaHOPBepInEx";
        public const string PluginVersion = "1.0.0";

        public override void Load()
        {
            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll();
            Log.LogInfo("megaHOP initialized.");
        }
    }

    [HarmonyPatch(typeof(PlayerInput), "Update")]
    public class StupidJumpDetectionPatch
    {
        public static bool IsHoldingJump;
        static void Postfix(PlayerInput __instance) => IsHoldingJump = __instance.IsHoldingJump();
    }

    [HarmonyPatch(typeof(MyPlayer), "Update")]
    public class MegaHOPPatch
    {
        static void Postfix(MyPlayer __instance)
        {
            if (__instance.playerMovement &&
                StupidJumpDetectionPatch.IsHoldingJump &&
                __instance.playerMovement.grounded)
            {
                __instance.playerMovement.Jump();
            }
        }
    }
}