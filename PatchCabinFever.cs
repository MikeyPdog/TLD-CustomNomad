using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(CabinFever))]
    [HarmonyPatch("Update")]
    class PatchCabinFever
    {
        static bool Prefix()
        {
            return false;
        }
    }
}