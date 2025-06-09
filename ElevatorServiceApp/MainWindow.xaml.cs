using System.Windows;
using MaterialDesignThemes.Wpf;

namespace ElevatorServiceApp
{
    public partial class MainWindow : Window
    {
        private bool _isDarkTheme = false;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ServiceRequestsPage());
            SetTheme(_isDarkTheme);
        }

        private void SetTheme(bool isDark)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void ToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            SetTheme(_isDarkTheme);
        }

        private void ServiceRequests_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServiceRequestsPage());
        }

        private void Elevators_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ElevatorsPage());
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientsPage());
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EmployeesPage());
        }

        private void Parts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PartsPage());
        }

        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}