using System;
using System.IO;
using System.Linq;
using Harmony;
using UnityEngine;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Panel_MainMenu))]
    [HarmonyPatch("OnSelectExperienceContinue")]
    class PatchDifficultyOnNomad
    {
        static bool Prepare()
        {
            FileLog.Log(DateTime.Today.ToShortDateString() + " ---- Loaded Nomad Mod.");
            try
            {
                var dic = File.ReadAllLines("mods/Nomad.txt")
                    .Where(l => !l.StartsWith("/"))
                    .Select(l => l.Split(new[] { '=' }))
                    .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

                Globals.SprintCaloriesMultiplier = float.Parse(dic["SprintCaloriesMultiplier"]);
                Globals.DaysToSpendNomad = float.Parse(dic["DaysToSpendNomad"]);
                Globals.ClothingRepairMultiplier = float.Parse(dic["ClothingRepairMultiplier"]);
            }
            catch (Exception e)
            {
                Debug.LogFormat(e.Message);
                FileLog.Log(e.Message);
                return false;
            }

            return true;
        }

        static void Prefix(Panel_MainMenu __instance)
        {
            if (GameManager.GetExperienceModeManagerComponent().GetCurrentExperienceModeType() == ExperienceModeType.ChallengeNomad)
            {
                GameManager.GetExperienceModeManagerComponent().SetExperienceModeType(ExperienceModeType.Stalker);
//                HUDMessage.AddMessage("Set to stalker!");
                Globals.NomadActive = true;
            }
            else
            {
                Globals.NomadActive = false;
            }
        }
    }
    
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("LoadSceneWithLoadingScreen")]
    class PatchLoadSceneWithLoadingScreen
    {
        static void Prefix()
        {
            if (Globals.NomadActive)
            {
                InterfaceManager.m_Panel_Loading.m_CommandToRunAfterLoad = "mission_jump chnmd false";
                InterfaceManager.m_Panel_Loading.m_SaveAfterLoad = true;
            }
        }
    }

    [HarmonyPatch(typeof(ExperienceModeManager))]
    [HarmonyPatch("Deserialize")]
    class PatchDeserialize
    {
        static void Postfix()
        {
            FileLog.Log("Deserialize postfix");
            if (GameManager.GetExperienceModeManagerComponent().GetCurrentExperienceModeType() == ExperienceModeType.ChallengeNomad)
            {
                GameManager.GetExperienceModeManagerComponent().SetExperienceModeType(ExperienceModeType.Stalker);
                FileLog.Log("Deserialize postfix - set to stalker");
                Globals.NomadActive = true;
            }
        }
    }
    
    [HarmonyPatch(typeof(Utils))]
    [HarmonyPatch("XPModeIsSandbox")]
    class PatchXPModeIsSandbox
    {
        static bool Prefix(ref bool __result)
        {
            // If nomad enabled, set result to false, also return false
            if (Globals.NomadActive)
            {
                __result = false;
                return false;
            }

            return true; // Use the game function
        }
    }

    [HarmonyPatch(typeof(Panel_PauseMenu))]
    [HarmonyPatch("SetExperienceModeIcon")]
    class PatchSetExperienceModeIcon
    {
        static void Postfix(Panel_PauseMenu __instance)
        {
            // If nomad enabled, set result to false, also return false
            if (Globals.NomadActive)
            {		
		        __instance.m_VoyageurIcon.gameObject.SetActive(false);
		        __instance.m_StalkerIcon.gameObject.SetActive(false);
		        __instance.m_NomadIcon.gameObject.SetActive(true);
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Log))]
    [HarmonyPatch("UpdateMissionsPage")]
    class PatchUpdateMissionsPage
    {
        static void Postfix(Panel_Log __instance)
        {
            if (Globals.NomadActive)
            {		
			    __instance.m_TimerObject.SetActive(false);
			    __instance.m_ChallengeTexture.mainTexture = (Texture2D)Resources.Load("LargeTextures/challenge_Nomad", typeof(Texture2D));
			    Utils.SetActive(__instance.m_ObjectiveTransform.gameObject, true);
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Log))]
    [HarmonyPatch("UpdateCurrentGameLabel")]
    class PatchUpdateCurrentGameLabel
    {
        static void Postfix(Panel_Log __instance)
        {
            if (Globals.NomadActive)
            {		
			    __instance.m_CurrentGameLabel.text = Localization.Get("GAMEPLAY_CurrentChallenge");
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Log))]
    [HarmonyPatch("RefreshActivePanelStates")]
    class PatchRefreshActivePanelStates
    {
        static void Postfix(Panel_Log __instance)
        {
            if (Globals.NomadActive)
            {		
			    __instance.m_CurrentGameLabel.text = Localization.Get("GAMEPLAY_CurrentChallenge");
            }
        }
    }
// NOTE: Nomad icon returned on Loading game. Check difficulty still there after resume!
}