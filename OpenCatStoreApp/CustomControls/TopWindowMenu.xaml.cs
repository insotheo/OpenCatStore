using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for TopWindowMenu.xaml
    /// </summary>
    public partial class TopWindowMenu : UserControl
    {
        public TopWindowMenu()
        {
            InitializeComponent();

            TopWindowGrid.MouseDown += WindowMove;

            MinWindowBtn.Click += (object sender, RoutedEventArgs args) =>
            {
                StandartView(true);
                Window.GetWindow(this).WindowState = WindowState.Minimized;
                StandartView(false);
            };

            MaxWindowBtn.Click += (object sender, RoutedEventArgs args) =>
            {
                Window window = Window.GetWindow(this);
                StandartView(true);
                if (window.WindowState == WindowState.Maximized)
                {
                    window.WindowState = WindowState.Normal;
                }
                else
                {
                    window.WindowState = WindowState.Maximized;
                }
                StandartView(false);
            };

            CloseWindowBtn.Click += (object sender, RoutedEventArgs args) =>
            {
                StandartView(true);
                Window.GetWindow(this).Close();
                StandartView(false);
            };
        }

        public void Init()
        {
            ResizeMode rm = Window.GetWindow(this).ResizeMode;
            switch (rm)
            {
                case ResizeMode.NoResize:
                    MinWindowBtn.Visibility = Visibility.Collapsed;
                    MaxWindowBtn.Visibility = Visibility.Collapsed;
                    break;

                case ResizeMode.CanMinimize:
                    MaxWindowBtn.Visibility = Visibility.Collapsed;
                    break;

                default:
                    break;
            }
        }

        private void WindowMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window.GetWindow(this).DragMove();
            }
        }

        private void StandartView(bool state)
        {
            Window window = Window.GetWindow(this);
            if (state)
            {
                window.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            else
            {
                window.WindowStyle = WindowStyle.None;
            }
        }

    }
}
