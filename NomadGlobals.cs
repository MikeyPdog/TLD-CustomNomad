using System;
using System.Collections.Generic;
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
                        .Where(arr => arr.Length == 2)
                        .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            SetGlobal<float>(dic, "SprintCaloriesMultiplier", x => SprintCaloriesMultiplier = x);
            SetGlobal<float>(dic, "DaysToSpendNomad", x => DaysToSpendNomad = x);
            SetGlobal<float>(dic, "ClothingRepairMultiplier", x => ClothingRepairMultiplier = x);
            SetGlobal<bool>(dic, "CabinFeverEnabled", x => CabinFeverEnabled = x);
            SetGlobal<float>(dic, "StarvationDamageMultiplier", x => StarvationDamageMultiplier = x);
            SetGlobal<float>(dic, "CalorieBurnRateMultiplier", x => CalorieBurnRateMultiplier = x);
            SetGlobal<ExperienceModeType>(dic, "Difficulty", x => Difficulty = x);

            var leftoverEntries = dic.Select(kvp => kvp.Key + "=" + kvp.Value).ToArray();
            if (leftoverEntries.Any())
            {
                var leftoverString = string.Join(", ", leftoverEntries);
                FileLog.Log("*** LINES FOUND WITHOUT MATCH :" + leftoverString);
            }

            FileLog.Log("Finished loading Nomad values.");
        }

        public static void SetGlobal<T>(Dictionary<string, string> dict, string key, Action<T> globalSetter)
        {
            string value;
            if (!dict.TryGetValue(key, out value))
            {
                FileLog.Log("* No entry for '" + key + "' found. Defaulting value.");
                return;
            }

            try
            {
                T val = typeof(T).IsEnum 
                    ? ParseToEnum<T>(value) 
                    : ParseTo<T>(value);

                FileLog.Log("* Setting " + key + " to " + val);
                dict.Remove(key);
                globalSetter(val);
            }
            catch (Exception e)
            {
                FileLog.Log("*** BAD VALUE for '" + key + "' ('" + value + "'). Defaulting value. Full error below:");
                FileLog.Log(e.Message);
            }
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
