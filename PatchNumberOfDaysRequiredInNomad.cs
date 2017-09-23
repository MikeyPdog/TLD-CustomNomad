using Harmony;
using UnityEngine.SceneManagement;

namespace NomadExtreme
{
    [HarmonyPatch(typeof(Action_NomadRequirements))]
    [HarmonyPatch("OnUpdate")]
    class PatchNumberOfDaysRequiredInNomad
    {
        private static bool Prefix(Action_NomadRequirements __instance)
        {
            NomadGlobals.NomadActive = true;
            __instance.daysToSpendInEach = NomadGlobals.DaysToSpendNomad;
            if (GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene != null)
            {
                string text = SceneManager.GetActiveScene().name + ":" + GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene;
                HUDMessage.AddMessage(text);
            }
            else
            {
                HUDMessage.AddMessage(SceneManager.GetActiveScene().name);
            }
            return true;
        }
    }

    // Maybe add: Hunting Lodge (Broken railroad)
}