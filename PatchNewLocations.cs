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

//            var newDict = JsonConvert.DeserializeObject<Dictionary<string, float>>(__instance.nomadDataSerialized.value);
//            foreach (var location in NewLocations)
//            {
//                if (!newDict.ContainsKey(location))
//                {
//                    FileLog.Log("Adding location:" + location);
//                    newDict.Add(location, 0);
//                }
//            }

            foreach (var location in NewLocations)
            {
                if (!__instance.scenesRequired.Contains(location))
                {
                    var fullTitle = GetSceneName(location);
                    FileLog.Log("Adding location:" + location + ". Localisation text:" + fullTitle);
//                    if (fullTitle.StartsWith("GAMEPLAY_"))
//                    {
//                        string sceneLocationLocIDToShow = GameManager.m_SceneTransitionData.m_SceneLocationLocIDToShow;
//                        string text = string.Empty;
//                        if (string.IsNullOrEmpty(sceneLocationLocIDToShow) && GameManager.GetWeatherComponent().IsIndoorEnvironment())
//                        {
//                            text = SceneNameMappingManager.GetNameForScene(SceneManager.GetActiveScene().name);
//                        }
//                    }

//                    GAMEPLAY_HuntingLodge = System.String[]
//GAMEPLAY_MaintenanceYard = System.String[]

//                    var allLocations = string.Join(", ", __instance.scenesRequired.ToArray());
//
//                    var joinedKvps = Localization.dictionary.Select(kvp => kvp.Key + " = " + kvp.Value);
//                    foreach (var joinedKvp in joinedKvps)
//                    {
//                        FileLog.Log(joinedKvp);
//                    }
//
//                    FileLog.Log("All locations:" + allLocations);
                    __instance.scenesRequired.Insert(0, location);
                }
            }

            // Add [PV] etc to start of menu text
            // Extend list so it can hold more locations
        }

        private static string GetSceneName(string location)
        {
            string locIDForScene = SceneNameMappingManager.GetLocIDForScene(location);
            if (!string.IsNullOrEmpty(locIDForScene))
            {
                return Localization.Get(locIDForScene);
            }
//            if (location == "HuntingLodgeA")
//            {
//                location = "HuntingLodge";
//            }
//            else if (location == "MaintenanceShedA")
//            {
//                location = "MaintenanceYard";
//            }
            return Localization.Get("GAMEPLAY_" + location);
        }
    }
}