using FreelanceExchange_desktop.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public Task SelectedTask { get; set; }
        private MainWindow _mainWindow;
        public HomePage(ObservableCollection<Task> tasks, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Tasks = tasks;
            _mainWindow = mainWindow;
        }

        public void Edit(object obj, MouseButtonEventArgs e)
        {
            ProxyWindow proxyWindow = new ProxyWindow(1, _mainWindow,SelectedTask);
            proxyWindow.Show();
        }
    }
}