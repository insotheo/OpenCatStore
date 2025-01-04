using OpenCatStoreAPI;
using System;
using System.Windows;

namespace OpenCatStoreApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                GithubAPI.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InitializeComponent();
            TopMenu.Init();
        }
    }
}
