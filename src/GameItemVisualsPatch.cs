using HarmonyLib;
using PaintCore;

namespace CozyToolBoost
{
    // SetGraduallyFadeSpeed assigns an absolute speed on every call, so scaling after it never compounds.
    [HarmonyPatch(typeof(CGRGameItemVisuals), nameof(CGRGameItemVisuals.SetGraduallyFadeSpeed))]
    internal static class GameItemVisualsPatch
    {
        private static void Postfix(CGRGameItemVisuals __instance)
        {
            float multiplier = Plugin.UvDurationMultiplier.Value;

            foreach (CwGraduallyFade fade in __instance.graduallyFadeComponents)
            {
                if (fade == null)
                {
                    continue;
                }

                fade.Speed = ToolBoostMath.ScaleFadeSpeed(fade.Speed, multiplier);
            }
        }
    }
}
