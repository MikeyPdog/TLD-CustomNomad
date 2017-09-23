using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Skill_ClothingRepair))]
    [HarmonyPatch("GetItemConditionScale")]
    class PatchClothingRepairMultiplier
    {
        static void Postfix(Skill_ClothingRepair __instance, ref float __result)
        {
            if (NomadGlobals.NomadActive)
            {
                __result *= NomadGlobals.ClothingRepairMultiplier;
            }
        }
    }
}