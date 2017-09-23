using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Condition))]
    [HarmonyPatch("Start")]
    class PatchStarvation
    {
        static void Postfix(Condition __instance)
        {
            if (NomadGlobals.NomadActive)
            {
                var m_StartHasBeenCalled = Traverse.Create(__instance).Field("m_StartHasBeenCalled").GetValue<bool>();
                if (m_StartHasBeenCalled)
                {
                    __instance.m_HPDecreasePerDayFromStarving *= NomadGlobals.StarvationDamageMultiplier;
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
            if (NomadGlobals.NomadActive)
            {
                __result *= NomadGlobals.CalorieBurnRateMultiplier;
            }
        }
    }
}