using FreelanceExchange_desktop.Data;
using System.Collections.Generic;
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
            AddCommand = new DelegateCommand(Delete);
            DeleteCommand = new DelegateCommand(Add);
        }

        public void Edit (object sender, MouseButtonEventArgs e)
        {

        }

        public void Delete(object obj)
        {

        }

        public void Add(object obj)
        {

        }
    }
}
