using MelonLoader;

namespace YarnLocalHooker
{
    internal class Config
    {
        private static MelonPreferences_Category category;
        private static MelonPreferences_Entry<string> filepath;
        private const string m_cfg = "UserData/loc.cfg";

        public static string FilePath => filepath.Value;

        public static void Init()
        {
            category = MelonPreferences.CreateCategory("General");
            category.SetFilePath(m_cfg, false);

            if (File.Exists(m_cfg))
            {
                category.LoadFromFile();
                filepath = category.GetEntry<string>("filepath");
            }

            if (filepath == null)
            {
                filepath = category.CreateEntry("filepath", "localization.csv");
                category.SaveToFile();
            }
        }

    }
}
