using System;
using System.IO;

namespace OpenCatStoreAPI
{
    public static class StoreConfig
    {
        public static string SaveAppsDirectory { get; private set; }
        public static bool ShowDirectoryOnFinish { get; private set; }
        public static string GithubAPIKey { get; private set; }

        public static void SetSaveAppDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"{path} not found!");
            }
            SaveAppsDirectory = path;
        }

        public static void SetShowDirectoryOnFinish(bool value) => ShowDirectoryOnFinish = value;

        public static void SetGithubAPIKey(string value)
        {
            GithubAPIKey = value;
            GithubAPI.UpdateToken();
        }

        public static void RestoreToDefault()
        {
            SaveAppsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "apps");
            if (!Directory.Exists(SaveAppsDirectory)) Directory.CreateDirectory(SaveAppsDirectory);

            ShowDirectoryOnFinish = true;

            GithubAPIKey = String.Empty;
        }

    }

    public struct StoreConfigData
    {
        public string SaveAppsDirectory;
        public bool ShowDirectoryOnFinish;
        public string GithubAPIKey;

        public StoreConfigData(string saveAppsDirectory, bool showDirectoryOnFinish, string githubAPIKey)
        {
            SaveAppsDirectory = saveAppsDirectory;
            ShowDirectoryOnFinish = showDirectoryOnFinish;
            GithubAPIKey = githubAPIKey;
        }
    }
}
