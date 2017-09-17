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
                HUDMessage.AddMessage("Set to stalker!");
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

// NOTE: Nomad icon returned on Loading game. Check difficulty still there after resume!
}