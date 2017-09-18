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


    public class Globals
    {
        public static float SprintCaloriesMultiplier = 5.0f;
        public static float DaysToSpendNomad = 5;
        public static float ClothingRepairMultiplier = 0.5f;
        public static bool CabinFever = false;
        public static float StarvationDamageMultiplier = 5;

        public static bool NomadActive { get; set; }
    }
}
