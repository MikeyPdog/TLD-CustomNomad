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

    // Maybe add: Hunting Lodge (Broken railroad)
}