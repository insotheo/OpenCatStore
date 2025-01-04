using System.IO;

namespace OpenCatStoreAPI
{
    public static class StoreConfig
    {
        public static string SaveAppsDirectory { get; private set; }
        public static bool ShowDirectoryOnFinish { get; private set; }

        public static void SetSaveAppDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"{path} not found!");
            }
            SaveAppsDirectory = path;
        }

        public static void SetShowDirectoryOnFinish(bool value) => ShowDirectoryOnFinish = value;

        public static void RestoreToDefault()
        {
            SaveAppsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "apps");
            if (!Directory.Exists(SaveAppsDirectory)) Directory.CreateDirectory(SaveAppsDirectory);

            ShowDirectoryOnFinish = true;
        }

    }

    public struct StoreConfigData
    {
        public string SaveAppsDirectory;
        public bool ShowDirectoryOnFinish;

        public StoreConfigData(string saveAppsDirectory, bool showDirectoryOnFinish)
        {
            SaveAppsDirectory = saveAppsDirectory;
            ShowDirectoryOnFinish = showDirectoryOnFinish;
        }
    }
}
