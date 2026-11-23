using HarmonyLib;
using PaintIn3D;

namespace CozyToolBoost
{
    // ApplyStats recomputes radius and dirt limit from the database on every call, so scaling after it never compounds.
    [HarmonyPatch(typeof(ToolStatsApplicator), "ApplyStats")]
    internal static class ToolStatsApplicatorPatch
    {
        private const string AirdrierCategory = "heatgun";

        private static void Postfix(ToolStatsApplicator __instance)
        {
            ScaleCleaningRadius(__instance);
            ScaleDirtLimit(__instance);
            ScaleAirdrierStrength(__instance);
        }

        private static void ScaleAirdrierStrength(ToolStatsApplicator tool)
        {
            if (tool.category != AirdrierCategory)
            {
                return;
            }

            ScaleStrength(tool.dirtDecal, tool.dirtSphere);
            ScaleStrength(tool.inkDecal, tool.inkSphere);
            ScaleStrength(tool.scratchesDecal, tool.scratchesSphere);
        }

        private static void ScaleStrength(CwPaintDecal decal, CwPaintSphere sphere)
        {
            float multiplier = Plugin.AirdrierStrengthMultiplier.Value;

            if (decal != null)
            {
                decal.Opacity = ToolBoostMath.ScalePower(decal.Opacity, multiplier);
                decal.Hardness = ToolBoostMath.ScalePower(decal.Hardness, multiplier);
            }

            if (sphere != null)
            {
                sphere.Opacity = ToolBoostMath.ScalePower(sphere.Opacity, multiplier);
                sphere.Hardness = ToolBoostMath.ScalePower(sphere.Hardness, multiplier);
            }
        }

        // The "dirtying" painters are intentionally left alone: they paint dirt onto the tool, not the item.
        private static void ScaleCleaningRadius(ToolStatsApplicator tool)
        {
            ScalePainter(tool.dirtDecal, tool.dirtSphere);
            ScalePainter(tool.inkDecal, tool.inkSphere);
            ScalePainter(tool.scratchesDecal, tool.scratchesSphere);
            ScalePainter(tool.uvDecal, tool.uvSphere);
        }

        private static void ScalePainter(CwPaintDecal decal, CwPaintSphere sphere)
        {
            float multiplier = Plugin.RadiusMultiplier.Value;

            if (decal != null)
            {
                decal.Radius = ToolBoostMath.ScaleRadius(decal.Radius, multiplier);
            }

            if (sphere != null)
            {
                sphere.Radius = ToolBoostMath.ScaleRadius(sphere.Radius, multiplier);
            }
        }

        private static void ScaleDirtLimit(ToolStatsApplicator tool)
        {
            ToolDirtinessTracker tracker = tool.dirtinessTracker;

            if (tracker == null)
            {
                return;
            }

            tracker.dirtinessLimit = ToolBoostMath.ScaleDirtLimit(tracker.dirtinessLimit, Plugin.DirtLimitMultiplier.Value);
            Plugin.Log.LogInfo($"{tool.category}: dirt limit {tracker.dirtinessLimit}");
        }
    }
}
