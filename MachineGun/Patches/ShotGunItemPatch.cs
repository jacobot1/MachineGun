using HarmonyLib;
using UnityEngine;

namespace MachineGun.Patches
{
    [HarmonyPatch(typeof(ShotgunItem))]
    internal class ShotgunItemPatch
    {
        // Set shots per second
        static float shotClock = 1 / MachineGunMod.configShotsPerSecond.Value;
        /*
        [HarmonyPatch("ItemActivate")]
        [HarmonyPrefix]
        static bool DeleteNormalBehaviorPatch(ShotgunItem __instance)
        {
            return false;
        }
        */

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void BlitzFirePatch(ShotgunItem __instance)
        {
            // Fire every (1 / ShotsPerSecond) seconds
            if ((MachineGunMod.ButtonInstance.FireShotgunKey.ReadValue<float>() >= 1f) && (__instance.playerHeldBy == GameNetworkManager.Instance.localPlayerController))
            {
                if (shotClock <= 0f)
                {
                    __instance.ItemActivate(used: false);
                    shotClock = 1 / MachineGunMod.configShotsPerSecond.Value;
                }
                shotClock -= Time.deltaTime;
            }
        }

        [HarmonyPatch("ShootGun")]
        [HarmonyPrefix]
        static bool EnsureAmmoPatch(ShotgunItem __instance)
        {
            // Make sure gun doesn't run out of ammo
            __instance.shellsLoaded = 2;
            return true;
        }
    }
}
