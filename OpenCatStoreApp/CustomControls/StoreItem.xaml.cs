using System.Windows.Controls;
using OpenCatStoreAPI;
using System.Windows;
using System;
using System.Diagnostics;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for StoreItem.xaml
    /// </summary>
    public partial class StoreItem : UserControl
    {
        private string description;
        private string author;

        public StoreItem(SearchResultInfo item)
        {
            InitializeComponent();

            description = item.Description == String.Empty ? "NONE" : item.Description;

            AppNameTB.Text = item.ProjectName;
            author = item.Author;
            LanguageTB.Text = "Made with: " + item.Lanugages;
            AuthorTB.Text = "Made by: " + item.Author;

            ViewDescriptionBtn.Click += ViewDescription;
            GoToGithubPageBtn.Click += GoToGithubPage;
        }

        private void GoToGithubPage(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri($"https://github.com/{author}/{AppNameTB.Text}/");
            Process.Start(new ProcessStartInfo()
            {
                FileName = uri.ToString(),
                UseShellExecute = true
            });
        }

        private void ViewDescription(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(description, "Description", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
