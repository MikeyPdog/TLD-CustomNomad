using System;
using System.IO;
using System.Linq;
using Harmony;

namespace NomadExtreme
{

    /*
     * TODO:
     * - Get values from XML - copy code from other guy
     * - Edit number of days required
     * 
     */

//    class Main
//    {
//        static Main()
//        {
//            var harmony = HarmonyInstance.Create("com.github.harmony.NomadExtremeFF");
//            harmony.PatchAll(Assembly.GetExecutingAssembly());
//        }
//
//        static bool Prepare(HarmonyInstance instance)
//        {
//            // startup stuff
//
//            return true;
//        }
//    }

    // Usually, you will have one class for each method that you want to patch. Inside that class, you define a combination of Prefix, Postfix or Transpiler methods. 


    public static class NomadGlobals
    {
        public static float SprintCaloriesMultiplier = 5.0f;
        public static float DaysToSpendNomad = 5;
        public static float ClothingRepairMultiplier = 0.5f;
        public static bool CabinFeverEnabled = false;
        public static float StarvationDamageMultiplier = 5;
        public static float CalorieBurnRateMultiplier = 0.5f;
        public static ExperienceModeType Difficulty = ExperienceModeType.Stalker;

        public static bool NomadActive { get; set; }

        public static void LoadFromFile()
        {
            var dic = File.ReadAllLines("mods/Nomad.txt")
                        .Where(l => !l.StartsWith("/"))
                        .Select(l => l.Split(new[] { '=' }))
                        .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            SprintCaloriesMultiplier    = float.Parse(dic["SprintCaloriesMultiplier"]);
            DaysToSpendNomad            = float.Parse(dic["DaysToSpendNomad"]);
            ClothingRepairMultiplier    = float.Parse(dic["ClothingRepairMultiplier"]);
            CabinFeverEnabled           = bool.Parse(dic["CabinFeverEnabled"]);
            StarvationDamageMultiplier  = float.Parse(dic["StarvationDamageMultiplier"]);
            Difficulty = (ExperienceModeType)Enum.Parse(typeof(ExperienceModeType), dic["Difficulty"]);

            FileLog.Log("Loaded nomad values:" + string.Join(", ", dic.Select(kvp => kvp.Key + ":" + kvp.Value).ToArray()));
        }

        public static T ParseString<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}
