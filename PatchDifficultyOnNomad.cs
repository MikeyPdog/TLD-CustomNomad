using System;
using System.Collections.Generic;
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
            FileLog.Log("");
            FileLog.Log(DateTime.Now + " ---- Loading Nomad Mod.");
            try
            {
                NomadGlobals.LoadFromFile();
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
                GameManager.GetExperienceModeManagerComponent().SetExperienceModeType(NomadGlobals.Difficulty);
                NomadGlobals.NomadActive = true;
            }
            else
            {
                NomadGlobals.NomadActive = false;
            }
        }
    }
    
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("LoadSceneWithLoadingScreen")]
    class PatchLoadSceneWithLoadingScreen
    {
        static void Prefix()
        {
            if (NomadGlobals.NomadActive)
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
            if (GameManager.GetExperienceModeManagerComponent().GetCurrentExperienceModeType() == ExperienceModeType.ChallengeNomad)
            {
                GameManager.GetExperienceModeManagerComponent().SetExperienceModeType(NomadGlobals.Difficulty);
                NomadGlobals.NomadActive = true;
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
            if (NomadGlobals.NomadActive)
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
            if (NomadGlobals.NomadActive)
            {		
		        __instance.m_PilgrimIcon.gameObject.SetActive(false);
		        __instance.m_VoyageurIcon.gameObject.SetActive(false);
		        __instance.m_StalkerIcon.gameObject.SetActive(false);
		        __instance.m_NightmareIcon.gameObject.SetActive(false);
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
            if (NomadGlobals.NomadActive)
            {		
			    __instance.m_TimerObject.SetActive(false);
			    __instance.m_ChallengeTexture.mainTexture = (Texture2D)Resources.Load("LargeTextures/challenge_Nomad", typeof(Texture2D));
                Utils.SetActive(__instance.m_ObjectiveTransform.gameObject, true);

                var nomadText = Localization.Get("GAMEPLAY_" + ExperienceModeType.ChallengeNomad);
                __instance.m_MissionNameLabel.text = nomadText;
                __instance.m_MissionNameHeaderLabel.text = nomadText;
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Log))]
    [HarmonyPatch("UpdateCurrentGameLabel")]
    class PatchUpdateCurrentGameLabel
    {
        static void Postfix(Panel_Log __instance)
        {
            if (NomadGlobals.NomadActive)
            {
                var nomadText = Localization.Get("GAMEPLAY_" + ExperienceModeType.ChallengeNomad);
                __instance.m_CurrentGameLabel.text = nomadText;
            }
        }
    }

    [HarmonyPatch(typeof(Panel_Log))]
    [HarmonyPatch("RefreshActivePanelStates")]
    class PatchRefreshActivePanelStates
    {
        static void Postfix(Panel_Log __instance)
        {
            if (NomadGlobals.NomadActive)
            {
                var m_ActiveStates = Traverse.Create(__instance).Field("m_ActiveStates").GetValue<List<PanelLogState>>();
			    m_ActiveStates.Remove(PanelLogState.DayListStats);
            }
        }
    }
}