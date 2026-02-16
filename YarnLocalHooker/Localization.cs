using Il2CppCsvHelper;
using Il2CppSystem.Globalization;
using MelonLoader;
namespace YarnLocalHooker
{
    internal class Localization
    {
        internal static void LoadCSV(string filepath, out Il2CppSystem.Collections.Generic.Dictionary<string, string> loc)
        {
            Il2CppSystem.IO.StreamReader reader = null;
            CsvReader csv = null;
            loc = null;

            if (!Il2CppSystem.IO.File.Exists(filepath))
            {
                Melon<Core>.Logger.Error($"{filepath} not found");
                return;
            }

            try
            {
                reader = new(filepath);
                csv = new(reader);
                loc = new();

                csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;

                while (csv.Read())
                {
                    string key = csv.GetField(0);
                    string value = csv.GetField(1);
                    loc.Add(key, value);
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.Message);
            }
            finally
            {
                csv?.Dispose();
                reader?.Dispose();
            }
        }

        internal static void GetLocalizedStringFromCSV(string filepath, string key, out string value)
        {
            Il2CppSystem.IO.StreamReader reader = null;
            CsvReader csv = null;
            value = null;

            if (!Il2CppSystem.IO.File.Exists(filepath))
            {
                MelonLogger.Error($"{filepath} not found");
                return;
            }

            try
            {
                reader = new(filepath);
                csv = new(reader);
                csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;

                while (csv.Read())
                {
                    string _key = csv.GetField(0);
                    string _value = csv.GetField(1);

                    if (_key == key)
                    {
                        value = _value;
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
                csv?.Dispose();
                reader?.Dispose();
            }
        }
    }
}
