using System;
using System.IO;
using System.Linq;
using Harmony;

namespace NomadExtreme
{
    public static class NomadGlobals
    {
        public static float SprintCaloriesMultiplier = 1.0f;
        public static float DaysToSpendNomad = 3;
        public static float ClothingRepairMultiplier = 1f;
        public static bool CabinFeverEnabled = true;
        public static float StarvationDamageMultiplier = 1;
        public static float CalorieBurnRateMultiplier = 1f;
        public static ExperienceModeType Difficulty = ExperienceModeType.Voyageur;

        public static bool NomadActive { get; set; }

        public static void LoadFromFile()
        {
            var dic = File.ReadAllLines("mods/CustomNomad.txt")
                        .Where(l => !l.StartsWith("/"))
                        .Select(l => l.Split(new[] { '=' }))
                        .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            SprintCaloriesMultiplier    = ParseTo<float>(dic["SprintCaloriesMultiplier"]);
            DaysToSpendNomad            = ParseTo<float>(dic["DaysToSpendNomad"]);
            ClothingRepairMultiplier    = ParseTo<float>(dic["ClothingRepairMultiplier"]);
            CabinFeverEnabled           = ParseTo<bool>(dic["CabinFeverEnabled"]);
            StarvationDamageMultiplier  = ParseTo<float>(dic["StarvationDamageMultiplier"]);
            Difficulty                  = ParseToEnum<ExperienceModeType>(dic["Difficulty"]);

            FileLog.Log("Loaded nomad values:" + string.Join(", ", dic.Select(kvp => kvp.Key + ":" + kvp.Value).ToArray()));
        }

        public static T ParseTo<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T ParseToEnum<T>(string input)
        {
            return (T)Enum.Parse(typeof(T), input);
        }
    }
}
