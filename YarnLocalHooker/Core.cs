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
            loc = new Il2CppSystem.Collections.Generic.Dictionary<string, string>();

            if (!Il2CppSystem.IO.File.Exists(Config.FilePath))
            {
                Melon<Core>.Logger.Error($"{Config.FilePath} not found");
                return;
            }
            var reader = new Il2CppSystem.IO.StreamReader(Config.FilePath);
            var csv = new CsvReader(reader);
            csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;

            while (csv.Read())
            {
                string key = csv.GetField(0);
                string value = csv.GetField(1);
                loc.Add(key, value);
            }

            csv.Dispose();
            reader.Dispose();
#endif
        }
    }

    [HarmonyPatch(typeof(Il2CppYarn.Unity.Localization), "GetLocalizedString", typeof(string))]
    public class GetLocalizedStringPatch
    {
        public static void Postfix(string __0, ref string __result)
        {
#if DEBUG
            Il2CppSystem.IO.StreamReader reader = null;
            CsvReader csv = null;
            try
            {
                if (!Il2CppSystem.IO.File.Exists(Config.FilePath))
                {
                    MelonLogger.Error($"{Config.FilePath} not found");
                    return;
                }
                reader = new(Config.FilePath);
                csv = new(reader);
                csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;

                while (csv.Read())
                {
                    string key = csv.GetField(0);
                    string value = csv.GetField(1);

                    if (key == __0)
                    {
                        __result = value;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.Message);
            }
            finally
            {
                if (csv != null)
                    csv.Dispose();
                
                if (reader != null)
                    reader.Dispose();
            }

#else
            Core.loc.TryGetValue(__0, out string value);
            if (value != null)
                __result = value;
#endif
        }
    }
}