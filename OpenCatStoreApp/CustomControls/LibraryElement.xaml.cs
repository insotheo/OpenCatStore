using OpenCatStoreAPI;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System;
using OpenCatStoreApp.Windows;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for LibraryElement.xaml
    /// </summary>
    public partial class LibraryElement : UserControl
    {
        private LibraryItem libItem;

        public LibraryElement(LibraryItem item)
        {
            InitializeComponent();

            libItem = item;

            AppNameTB.Text = item.AppName;
            AuthorNameTB.Text = "Made by " + libItem.Author;
            LanguagesTB.Text = "Made with " + item.Languages;
            LicenseTB.Text = "License: " +  item.License;
            InLibSinceTB.Text = "In library since " + item.InLibrarySince.ToString("d");

            GoToGuthubPageBtn.Click += GoToGuthubPage;
            RemoveFromLibBtn.Click += RemoveFromLib;
            ViewVersionsBtn.Click += ViewVersions;
        }

        private void ViewVersions(object sender, RoutedEventArgs e)
        {
            try
            {
                using (VersionSelectorWindow dialog = new VersionSelectorWindow(libItem.Author, libItem.AppName))
                {
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveFromLib(object sender, System.Windows.RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to remove \"{libItem.AppName}\" from your library?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    if(Window.GetWindow(this) is MainWindow window)
                    {
                        window.LibPage.Remove(libItem);
                    }
                    else
                    {
                        throw new Exception("Can't handle removing item!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GoToGuthubPage(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = $"https://github.com/{libItem.Author}/{libItem.AppName}/"
            });
        }
    }
}
