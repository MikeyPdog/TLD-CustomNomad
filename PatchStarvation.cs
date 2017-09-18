﻿using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Condition))]
    [HarmonyPatch("Start")]
    class PatchStarvation
    {
        static void Postfix(Condition __instance)
        {
            if (Globals.NomadActive)
            {
                var m_StartHasBeenCalled = Traverse.Create(__instance).Field("m_StartHasBeenCalled").GetValue<bool>();
                if (m_StartHasBeenCalled)
                {
                    __instance.m_HPDecreasePerDayFromStarving *= Globals.StarvationDamageMultiplier;
                }
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager))]
    [HarmonyPatch("CalculateModifiedCalorieBurnRate")]
    class PatchCalculateModifiedCalorieBurnRate
    {
        static void Postfix(ref float __result)
        {
            if (Globals.NomadActive)
            {
                __result *= Globals.CalorieBurnRateMultiplier;
            }
        }
    }
}