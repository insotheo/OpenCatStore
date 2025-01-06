using OpenCatStoreAPI;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Diagnostics;
using System.IO;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for ReleaseItem.xaml
    /// </summary>
    public partial class ReleaseItem : UserControl
    {
        private AppRelease rel;

        public ReleaseItem(AppRelease release)
        {
            InitializeComponent();

            rel = release;

            VersionNameTB.Text = release.VersionName;

            foreach(string name in release.Names)
            {
                AssetsCB.Items.Add(name);
            }

            AssetsCB.SelectionChanged += SelectedAssetsChanged;
            ViewDescriptionBtn.Click += (object sender, RoutedEventArgs args) => { MessageBox.Show(rel.Description, "Description", MessageBoxButton.OK, MessageBoxImage.Information); };
            DownloadBtn.Click += Download;
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            if(AssetsCB.SelectedItem == null)
            {
                MessageBox.Show("Please, select asset to download!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int index = AssetsCB.SelectedIndex;
            try
            {
                rel.Download(index);
                if (StoreConfig.SendNotificationOnFinish)
                {
                    Notificator.SendNotification($"{rel.AppName} downloading finished!");
                }
                if (StoreConfig.ShowDirectoryOnFinish)
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        UseShellExecute = true, 
                        FileName = "explorer",
                        Arguments = Path.Combine(StoreConfig.SaveAppsDirectory, $"{rel.AppName}_{rel.VersionName}"),
                    });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectedAssetsChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = AssetsCB.SelectedIndex;
            SizeTB.Text = "Size: " + rel.Sizes[index].ToString() + "Kb";
        }
    }
}
