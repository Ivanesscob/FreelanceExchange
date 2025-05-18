using ControlzEx.Theming;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private bool _isDarkTheme = false;
        public MainWindow MainWindow { get; }
        public ICommand ChangeThemeCommand { get; }
        public ICommand ExitCommand { get; }

        public Settings(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            MainWindow = mainWindow;
            ChangeThemeCommand = new DelegateCommand(ChangeTheme);
            ExitCommand = new DelegateCommand(Exit);
        }

        private void ChangeTheme(object obj)
        {
            var theme = _isDarkTheme ? ThemeManager.Current.GetTheme("Light.Blue") : ThemeManager.Current.GetTheme("Dark.Blue");
            ThemeManager.Current.ChangeTheme(MainWindow, theme);
            _isDarkTheme = !_isDarkTheme;
        }

        private void Exit(object obj)
        {
            
        }
    }
}
