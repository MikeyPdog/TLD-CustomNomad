using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Action_NomadRequirements))]
    [HarmonyPatch("OnUpdate")]
    class PatchNumberOfDaysRequiredInNomad
    {
        // Call the other patched method to get the difficulty for nomad
        static bool Prefix(Action_NomadRequirements __instance)
        {
//            HUDMessage.AddMessage("Still nomad!", 1f, true);
            Globals.NomadActive = true;
            __instance.daysToSpendInEach = Globals.DaysToSpendNomad;
            return true;
        }
    }
}