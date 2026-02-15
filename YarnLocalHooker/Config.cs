using MelonLoader;

namespace YarnLocalHooker
{
    internal class Config
    {
        private static MelonPreferences_Category category;
        private static MelonPreferences_Entry<string> filepath;
        private const string m_cfg = "UserData/loc.cfg";
        private const string m_deffilepath = "localization.csv";

        public static string FilePath => filepath.Value;

        public static void Init()
        {
            category = MelonPreferences.CreateCategory("YarnLocalHooker");
            category.SetFilePath(m_cfg, false);

            category.LoadFromFile();

            if (category.HasEntry("filepath"))
            {
                filepath = category.GetEntry<string>("filepath");
            }
            else
            {
                filepath = category.CreateEntry("filepath", m_deffilepath);
                category.SaveToFile();
            }
        }
    }
}
