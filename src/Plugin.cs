using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace CozyToolBoost
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "local.cozygame.toolboost";
        public const string PluginName = "Cozy Tool Boost";
        public const string PluginVersion = "1.0.0";

        private const string ConfigSection = "Tools";
        private const string EconomySection = "Economy";

        internal static ConfigEntry<float> RadiusMultiplier;
        internal static ConfigEntry<float> DirtLimitMultiplier;
        internal static ConfigEntry<float> UvDurationMultiplier;
        internal static ConfigEntry<float> AirdrierStrengthMultiplier;
        internal static ConfigEntry<float> TweezersStrengthMultiplier;
        internal static ConfigEntry<float> SRankSaleMultiplier;
        internal static BepInEx.Logging.ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            RadiusMultiplier = BindMultiplier("RadiusMultiplier", 2.5f, "Multiplies the cleaning radius (area of effect) of every tool");
            DirtLimitMultiplier = BindMultiplier("DirtLimitMultiplier", 6f, "Multiplies how much dirt a tool absorbs before it is too dirty (6 = gets dirty 6x slower per pixel cleaned)");
            UvDurationMultiplier = BindMultiplier("UvDurationMultiplier", 3f, "Multiplies how long the UV light reveal stays visible (3 = fades 3x slower)");
            AirdrierStrengthMultiplier = BindMultiplier("AirdrierStrengthMultiplier", 1.5f, "Multiplies the airdrier (hair dryer) strength: opacity and hardness of its dirt, ink and scratches cleaning");
            TweezersStrengthMultiplier = BindMultiplier("TweezersStrengthMultiplier", 2f, "Multiplies the tweezers pull strength (2 = stickers release at half the pull force)");
            SRankSaleMultiplier = Config.Bind(EconomySection, "SRankSaleMultiplier", 1.75f, new ConfigDescription("Sale price multiplier for a 100% cleaned (rank S) cartridge, applied on top of its list price. The game default is 1.15", new AcceptableValueRange<float>(0.1f, 20f)));

            new Harmony(PluginGuid).PatchAll();
            Log.LogInfo($"{PluginName} loaded: radius x{RadiusMultiplier.Value}, dirt limit x{DirtLimitMultiplier.Value}, UV duration x{UvDurationMultiplier.Value}, airdrier strength x{AirdrierStrengthMultiplier.Value}, tweezers strength x{TweezersStrengthMultiplier.Value}, S-rank sale x{SRankSaleMultiplier.Value}");
        }

        private ConfigEntry<float> BindMultiplier(string key, float defaultValue, string description)
        {
            var range = new AcceptableValueRange<float>(0.1f, 20f);

            return Config.Bind(ConfigSection, key, defaultValue, new ConfigDescription(description, range));
        }
    }
}
