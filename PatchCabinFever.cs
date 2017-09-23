using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(CabinFever))]
    [HarmonyPatch("Update")]
    class PatchCabinFever
    {
        static bool Prefix()
        {
            if (Globals.NomadActive && Globals.CabinFeverEnabled == false)
            {
                return false;
            }

            return true;
        }
    }
}