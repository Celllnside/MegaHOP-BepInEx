using MelonLoader;
using HarmonyLib;


using Il2Cpp;
using UnityEngine;
using Il2CppAssets.Scripts.Actors.Player;
[assembly: MelonInfo(typeof(MegaHOP.Core), "MegaHOP", "1.0.0", "Strok", null)]
[assembly: MelonGame("Ved", "Megabonk")]

namespace MegaHOP
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }

    
    [HarmonyPatch(typeof(PlayerInput), "Update")]
    public class StupidJumpDetectionPatch
    {
        public static bool IsHoldingJump;
        static void Postfix(PlayerInput __instance)
        {
            IsHoldingJump = __instance.IsHoldingJump();
        }
    }

    [HarmonyPatch(typeof(MyPlayer), "Update")]
    public class MegaHOPPatch
    {
        static void Postfix(MyPlayer __instance)
        {
            if (__instance.playerMovement)
            {
                if (StupidJumpDetectionPatch.IsHoldingJump && __instance.playerMovement.grounded)
                {
                    __instance.playerMovement.Jump();
                }
            }
        }
    }
}