using Il2CppCsvHelper;
using Il2CppSystem.Globalization;
using MelonLoader;
namespace YarnLocalHooker
{
    internal class Localization
    {
        internal static void LoadCSV(string filepath, out Il2CppSystem.Collections.Generic.Dictionary<string, string> loc)
        {
            loc = null;
            var reader = new Il2CppSystem.IO.StreamReader(filepath);
            var csv = new CsvReader(reader);
            try
            {
                loc = new();

                if (!Il2CppSystem.IO.File.Exists(filepath))
                {
                    Melon<Core>.Logger.Error($"{filepath} not found");
                    return;
                }
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
                csv.Dispose();
                reader.Dispose();
            }
        }

        internal static void GetLocalizedStringFromCSV(string filepath, string key, out string value)
        {
            Il2CppSystem.IO.StreamReader reader = null;
            CsvReader csv = null;
            value = null;
            try
            {
                if (!Il2CppSystem.IO.File.Exists(filepath))
                {
                    MelonLogger.Error($"{filepath} not found");
                    return;
                }
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
                if (csv != null)
                    csv.Dispose();

                if (reader != null)
                    reader.Dispose();
            }
        }
    }
}
