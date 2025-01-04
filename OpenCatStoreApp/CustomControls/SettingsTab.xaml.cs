using Newtonsoft.Json;
using OpenCatStoreAPI;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for SettingsTab.xaml
    /// </summary>
    public partial class SettingsTab : UserControl
    {
        private readonly string path = Path.Combine(Directory.GetCurrentDirectory(), "config.conf");

        public SettingsTab()
        {
            InitializeComponent();
            if (!File.Exists(path))
            {
                StoreConfig.RestoreToDefault();
                using (FileStream fs = File.Create(path)) { }
                Save();
            }
            else
            {
                Load();
            }
            UpdateView();

            SaveSettingsBtn.Click += (object sender, RoutedEventArgs args) => { Save(); };
            RestoreSettingToDefaultBtn.Click += (object sender, RoutedEventArgs args) => { StoreConfig.RestoreToDefault(); UpdateView(); };
        }

        public void UpdateView()
        {
            AppDirectoryFolder.Text = StoreConfig.SaveAppsDirectory;
            ShowDirectoryCB.IsChecked = StoreConfig.ShowDirectoryOnFinish;
            GithubAPIKeyPB.Password = StoreConfig.GithubAPIKey;
        }

        public void Save()
        {
            StoreConfigData data = new StoreConfigData()
            {
                SaveAppsDirectory = AppDirectoryFolder.Text,
                ShowDirectoryOnFinish = (bool)ShowDirectoryCB.IsChecked,
                GithubAPIKey = GithubAPIKeyPB.Password
            };
            string content = JsonConvert.SerializeObject(data, Formatting.Indented);
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path)) { }
            }
            File.WriteAllText(path, String.Empty);
            using (FileStream file = File.OpenWrite(path))
            {
                file.Seek(0, SeekOrigin.Begin);
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                file.Write(buffer, 0, buffer.Length);
            }
        }

        private void Load()
        {
            try
            {
                StoreConfigData data = JsonConvert.DeserializeObject<StoreConfigData>(File.ReadAllText(path));
                StoreConfig.SetSaveAppDirectory(data.SaveAppsDirectory);
                StoreConfig.SetShowDirectoryOnFinish(data.ShowDirectoryOnFinish);
                StoreConfig.SetGithubAPIKey(data.GithubAPIKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
