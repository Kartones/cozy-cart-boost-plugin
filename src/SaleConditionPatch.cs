using HarmonyLib;

namespace CozyToolBoost
{
    // The game's multiplier is a pure function of the rank, so replacing the result never compounds.
    [HarmonyPatch(typeof(GameItem), "getPriceConditionMultiplier")]
    internal static class SaleConditionPatch
    {
        private static void Postfix(GameItem __instance, ref float __result)
        {
            string rank = __instance.cleaningStats.cleaningRank;

            __result = ToolBoostMath.ResolveConditionMultiplier(rank, __result, Plugin.SRankSaleMultiplier.Value);
        }
    }
}
