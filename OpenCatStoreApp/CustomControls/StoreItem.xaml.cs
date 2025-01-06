using System.Windows.Controls;
using OpenCatStoreAPI;
using System.Windows;
using System;
using System.Diagnostics;
using OpenCatStoreApp.Windows;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for StoreItem.xaml
    /// </summary>
    public partial class StoreItem : UserControl
    {
        private string description;
        private string author;
        private string license;
        private string langs;

        public StoreItem(SearchResultInfo item)
        {
            InitializeComponent();

            description = item.Description == String.Empty ? "NONE" : item.Description;
            license = item.License;
            langs = item.Lanugages;

            AppNameTB.Text = item.ProjectName;
            author = item.Author;
            LanguageTB.Text = "Made with: " + item.Lanugages;
            AuthorTB.Text = "Made by: " + item.Author;
            LicenseTB.Text = "LICENSE: " + item.License;

            ViewDescriptionBtn.Click += ViewDescription;
            GoToGithubPageBtn.Click += GoToGithubPage;
            AddToLibBtn.Click += AddToLibrary;
        }

        private void AddToLibrary(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure, you want to add \"{AppNameTB.Text}\" to your library?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    LibraryItem libItem = Library.MakeLibraryItem(AppNameTB.Text, author, langs, license);
                    if (Window.GetWindow(this) is MainWindow window)
                    {
                        window.LibPage.AddLibItem(libItem);
                    }
                    else
                    {
                        throw new Exception("Can't add new item to your library!");
                    }
                    MessageBox.Show("Success!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
