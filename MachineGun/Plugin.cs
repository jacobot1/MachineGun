using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using MachineGun.Patches;
using BepInEx.Configuration;

namespace MachineGun
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.HardDependency)]
    public class MachineGunMod : BaseUnityPlugin
    {
        // Mod metadata
        private const string modGUID = "jacobot5.MachineGun";
        private const string modName = "MachineGun";
        private const string modVersion = "0.2.0";

        // Initalize Harmony
        private readonly Harmony harmony = new Harmony(modGUID);

        // Create static instance
        private static MachineGunMod Instance;

        // Create static instance of input class
        internal static MachineGunButton ButtonInstance;

        // Configuration
        public static ConfigEntry<float> configShotsPerSecond;
        public static ConfigEntry<float> configKnockbackForce;

        // Initialize logging
        public static ManualLogSource mls;

        private void Awake()
        {
            // Ensure static instance
            if (Instance == null)
            {
                Instance = this;
            }

            ButtonInstance = new MachineGunButton();

            // Send alive message
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("MachineGun has awoken.");

            // Bind configuration
            configShotsPerSecond = Config.Bind("General.Toggles",
                                                "ShotsPerSecond",
                                                4f,
                                                "How many times to shoot the Shotgun per second. Default is 4.");

            configKnockbackForce = Config.Bind("General.Toggles",
                                                "KnockbackForce",
                                                4f,
                                                "How much knockback force to apply. Default is 4.");

            // Do the patching
            harmony.PatchAll(typeof(MachineGunMod));
            harmony.PatchAll(typeof(ShotgunItemPatch));
        }
    }
}
