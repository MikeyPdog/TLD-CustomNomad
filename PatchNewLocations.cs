using System.Collections.Generic;
using System.Linq;
using Harmony;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

namespace NomadExtreme
{
    class PatchNewLocations
    {
        private static readonly List<string> NewLocations = new List<string>()
        {
            "MaintenanceShedA",
            "HuntingLodgeA",
            "MarshRegion"
        };

        public static void AddNewLocations(Action_NomadRequirements __instance)
        {
            if (__instance.scenesRequired == null)
            {
                return;
            }

            foreach (var location in NewLocations)
            {
                if (!__instance.scenesRequired.Contains(location))
                {
                    var fullTitle = GetSceneName(location);
                    FileLog.Log("Adding location:" + location + ". Localisation text:" + fullTitle);

                    __instance.scenesRequired.Insert(0, location);
                }
            }

            // Extend list so it can hold more locations
        }

        private static string GetSceneName(string location)
        {
            string locIDForScene = SceneNameMappingManager.GetLocIDForScene(location);
            if (!string.IsNullOrEmpty(locIDForScene))
            {
                return Localization.Get(locIDForScene);
            }

            return Localization.Get("GAMEPLAY_" + location);
        }
    }
}