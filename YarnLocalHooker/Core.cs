using HarmonyLib;
using Il2CppCsvHelper;
using Il2CppSystem.Globalization;
using MelonLoader;

[assembly: MelonInfo(typeof(YarnLocalHooker.Core), "YarnLocalHooker", "1.0.0", "IRUZZ", null)]
[assembly: MelonGame("Nino", "project_nonmp")]

namespace YarnLocalHooker
{
    public class Core : MelonMod
    {
        internal static Il2CppSystem.Collections.Generic.Dictionary<string, string> loc;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            Config.Init();

#if RELEASE
            Localization.LoadCSV(Config.FilePath, out loc);
#endif
        }
    }

    [HarmonyPatch(typeof(Il2CppYarn.Unity.Localization), "GetLocalizedString", typeof(string))]
    public class GetLocalizedStringPatch
    {
        public static void Postfix(string __0, ref string __result)
        {
#if DEBUG
            Localization.GetLocalizedStringFromCSV(Config.FilePath, __0, out string value);
            if (value != null)
                __result = value;
#else
            Core.loc.TryGetValue(__0, out string value);
            if (value != null)
                __result = value;
#endif
        }
    }
}