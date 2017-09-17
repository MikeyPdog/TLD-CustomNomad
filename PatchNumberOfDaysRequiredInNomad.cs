using Harmony;
using UnityEngine;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Action_NomadRequirements))]
    [HarmonyPatch("OnUpdate")]
    class PatchNumberOfDaysRequiredInNomad
    {
        static bool Prefix(Action_NomadRequirements __instance)
        {
            Globals.NomadActive = true;
            __instance.daysToSpendInEach = Globals.DaysToSpendNomad;
            return true;
        }
    }
}