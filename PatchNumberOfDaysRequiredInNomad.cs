using Harmony;
using UnityEngine.SceneManagement;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Action_NomadRequirements))]
    [HarmonyPatch("OnUpdate")]
    class PatchNumberOfDaysRequiredInNomad
    {
        private static bool Prefix(Action_NomadRequirements __instance)
        {
            NomadGlobals.NomadActive = true;
            __instance.daysToSpendInEach = NomadGlobals.DaysToSpendNomad;
            return true;
        }
    }

    [HarmonyPatch(typeof(Localization))]
    [HarmonyPatch("Get")]
    class PatchLocalization
    {
        private static void Postfix(string key, ref string __result)
        {
            if (key == "GAMEPLAY_NomadChallengeLoadText" || key == "GAMEPLAY_NomadChallengeObjective1")
            {
                __result = __result.Replace("3", NomadGlobals.DaysToSpendNomad.ToString());
            }
        }
    }
}