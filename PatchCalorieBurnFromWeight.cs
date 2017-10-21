using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(PlayerManager))]
    [HarmonyPatch("CalculateModifiedCalorieBurnRate")]
    class PatchCalorieBurnFromWeight
    {
        static void Postfix(PlayerManager __instance, ref float __result)
        {
            if (NomadGlobals.NomadActive)
            {
//                if (__instance.PlayerIsSprinting() || __instance.PlayerIsStriding() || __instance.PlayerIsWalking() || __instance.PlayerIsClimbing())
//                {
                var encumberComponent = GameManager.GetEncumberComponent();
                var m_GearWeightKG = Traverse.Create(encumberComponent).Field("m_GearWeightKG").GetValue<float>();
                if (m_GearWeightKG > encumberComponent.m_NoSprintCarryCapacityKG)
                {
                    __result *= NomadGlobals.HighEncumberanceCalorieMultiplier;
                }
                else if (encumberComponent.IsEncumbered())
                {
                    __result *= NomadGlobals.EncumberanceCalorieMultiplier;
                }
//                }
            }
        }
    }
}