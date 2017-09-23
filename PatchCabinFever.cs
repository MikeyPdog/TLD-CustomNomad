using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(CabinFever))]
    [HarmonyPatch("Update")]
    class PatchCabinFever
    {
        static bool Prefix()
        {
            if (NomadGlobals.NomadActive && NomadGlobals.CabinFeverEnabled == false)
            {
                return false;
            }

            return true;
        }
    }
}