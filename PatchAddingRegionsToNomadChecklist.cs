using System.Collections.Generic;
using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Action_NomadRequirements))]
    [HarmonyPatch("GetChecklistNameForKey")]
    public class PatchAddingRegionsToNomadChecklist
    {
        private static readonly HashSet<string> MissingMappings = new HashSet<string>();
        private static readonly Dictionary<string, string> ChecklistMappings = new Dictionary<string, string>()
        {
            {"Camp Office", "[ML] Camp Office"},
            {"Trapper's Cabin", "[ML] Trapper's Cabin"},
            {"Forestry Lookout", "[ML] Forestry Lookout"},
            {"Upper Dam/Lower Dam", "[ML] Dam interior"},
            {"Barn", "[PV] Barn"},
            {"Radio Control Hut", "[PV] Radio Control Hut"},
            {"Skeeter's Ridge (Basement)", "[PV] Skeeter's Ridge (Basement)"},
            {"Rural Store", "[PV] Crossroads Rural Store"},
            {"Quonset Garage", "[CH] Quonset Garage"},
            {"Abandoned Lookout", "[CH] Abandoned Lookout"},
            {"Mountaineer's Hut", "[TM] Mountaineer's Hut"},
            {"Lonely Lighthouse", "[DP] Lonely Lighthouse"},
            {"Stone Church", "[DP] Stone Church"},
            {"Cinder Hills Coal Mine", "[PV/CH] Cinder Hills Coal Mine"},
            {"Crumbling Highway (Basement)", "[CH/DP] Crumbling Highway (Basement)"},
            {"GAMEPLAY_HuntingLodgeA", "[BR] Hunting Lodge"},
            {"Forlorn Muskeg", "[FM] Forlorn Muskeg"},
            {"GAMEPLAY_MaintenanceShedA", "[BR] Maintenance Shed"}
        };

        private static void Postfix(ref string __result)
        {
            if (ChecklistMappings.ContainsKey(__result))
            {
                __result = ChecklistMappings[__result];
                if (NomadGlobals.RegionPrefixHints == false)
                {
                    __result = __result.Split(']')[1].Trim();
                }
            }
            else
            {
                if (MissingMappings.Contains(__result))
                {
                    return;
                }

                MissingMappings.Add(__result);
            }
        }
    }
}