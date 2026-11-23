using System;

namespace CozyToolBoost
{
    public static class ToolBoostMath
    {
        public static float ScaleRadius(float radius, float multiplier)
        {
            return radius * multiplier;
        }

        public static float ScaleFadeSpeed(float fadeSpeed, float durationMultiplier)
        {
            return fadeSpeed / durationMultiplier;
        }

        public static float ScalePower(float power, float multiplier)
        {
            return power * multiplier;
        }

        public static float ScaleBreakThreshold(float breakThreshold, float strengthMultiplier)
        {
            return breakThreshold / strengthMultiplier;
        }

        public const string TopCleaningRank = "S";

        public static float ResolveConditionMultiplier(string rank, float gameMultiplier, float topRankMultiplier)
        {
            return rank == TopCleaningRank ? topRankMultiplier : gameMultiplier;
        }

        public static int ScaleDirtLimit(int dirtLimit, float multiplier)
        {
            double scaled = (double)dirtLimit * multiplier;

            return (int)Math.Min(scaled, int.MaxValue);
        }
    }
}
