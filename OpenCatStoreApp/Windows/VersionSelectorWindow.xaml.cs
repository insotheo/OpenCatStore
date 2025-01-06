using System;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using OpenCatStoreAPI;
using OpenCatStoreApp.CustomControls;

namespace OpenCatStoreApp.Windows
{
    /// <summary>
    /// Interaction logic for VersionSelectorWindow.xaml
    /// </summary>
    public partial class VersionSelectorWindow : Window, IDisposable
    {
        public VersionSelectorWindow(string author, string appName)
        {
            InitializeComponent();
            TopMenu.Init();

            List<AppRelease> releases = Task.Run(() =>GithubAPI.GetListOfReleases(author, appName)).Result;

            foreach (AppRelease release in releases)
            {
                VersionsLB.Items.Add(new ReleaseItem(release));
            }
        }

        public void Dispose()
        {
            VersionsLB.Items.Clear();
        }
    }
}
