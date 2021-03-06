﻿using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(PlayerManager))]
    [HarmonyPatch("UpdateCalorieBurnRate")]
    class PatchSprintingCalorieBurnRate
    {
        static void Postfix(PlayerManager __instance)
        {
            if (NomadGlobals.NomadActive && __instance.PlayerIsSprinting())
            {
                var baseBurnRate = GameManager.GetHungerComponent().m_CalorieBurnPerHourSprinting * GameManager.GetFeatFreeRunner().GetSprintCalorieBurnScale();
                baseBurnRate *= NomadGlobals.SprintCaloriesMultiplier;
                var calorieBurnPerHour = __instance.CalculateModifiedCalorieBurnRate(baseBurnRate);
                GameManager.GetHungerComponent().SetCalorieBurnPerHour(calorieBurnPerHour);
            }
        }
    }
}