using HarmonyLib;

namespace CozyToolBoost
{
    // breakForce is set by GameStickerBuilder before Start, and CreatePins runs once per sticker, so this never compounds.
    [HarmonyPatch(typeof(CGR_ObiCloth_autopinStickiness), "CreatePins")]
    internal static class StickerPinPatch
    {
        private static void Prefix(CGR_ObiCloth_autopinStickiness __instance)
        {
            __instance.breakForce = ToolBoostMath.ScaleBreakThreshold(__instance.breakForce, Plugin.TweezersStrengthMultiplier.Value);
        }
    }
}
