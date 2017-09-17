using Harmony;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Panel_MainMenu))]
    [HarmonyPatch("OnSelectExperienceContinue")]
    class PatchDifficultyOnNomad
    {
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