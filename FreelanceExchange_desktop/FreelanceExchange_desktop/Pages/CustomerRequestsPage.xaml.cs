using FreelanceExchange_desktop.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for CustomerRequestsPage.xaml
    /// </summary>
    public partial class CustomerRequestsPage : Page
    {
        public List<Task> UserTasks { get; set; }
        public Task SelectedTask { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private MainWindow _mainWindow;
        public CustomerRequestsPage(List<Task> userTasks, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            UserTasks = userTasks;
            _mainWindow = mainWindow;
            DeleteCommand = new DelegateCommand(Delete, _ => SelectedTask != null);
            AddCommand = new DelegateCommand(Add);
        }

        public void Edit (object sender, MouseButtonEventArgs e)
        {
            ProxyWindow proxyWindow = new ProxyWindow(0, _mainWindow, SelectedTask);
            proxyWindow.Show();
        }

        public void Delete(object obj)
        {
            if (MessageBox.Show("Подтверждение удаления", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) { _mainWindow.Tasks.Remove(SelectedTask); DatabaseCommands.DeleteTaskFromDb(SelectedTask); _mainWindow.MainFrame.Navigate(new CustomerRequestsPage(_mainWindow.Tasks.Where(a => a.CreatorId == _mainWindow.CurrentUser.Id).ToList(), _mainWindow)); }
        }

        public void Add(object obj)
        {
            _mainWindow.MainFrame.Navigate(new CreateTaskPage(_mainWindow));
        }
    }
}
