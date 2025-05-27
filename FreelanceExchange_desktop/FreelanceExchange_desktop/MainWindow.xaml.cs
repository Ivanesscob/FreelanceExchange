using FreelanceExchange_desktop.Data;
using FreelanceExchange_desktop.Pages;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FreelanceExchange_desktop
{
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        public ObservableCollection<User> Users;
        public User CurrentUser;
        public ObservableCollection<Task> Tasks;
        public ObservableCollection<Response> Responses;

        private bool isCustomer;
        public bool IsCustomer
        {
            get => isCustomer;
            set
            {
                if (isCustomer != value)
                {
                    isCustomer = value;
                    OnPropertyChanged(nameof(IsCustomer));
                }
            }
        }

        private bool isfreelancer;
        public bool Isfreelancer
        {
            get => isfreelancer;
            set
            {
                if (isfreelancer != value)
                {
                    isfreelancer = value;
                    OnPropertyChanged(nameof(Isfreelancer));
                }
            }
        }

        //private bool _isVisibleAuth = true;
        //public bool IsVisibleAuth
        //{
        //    get => _isVisibleAuth;
        //    set
        //    {
        //        if (_isVisibleAuth != value)
        //        {
        //            _isVisibleAuth = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            OverlayFrame.Navigate(new Pages.Auth.Signin(OverlayFrame, this));
            HamburgerMenu.ItemInvoked += HamburgerMenu_ItemInvoked;

            //LoadData();
        }

        //private void LoadData()
        //{
            
        //}

        private void HamburgerMenu_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            if (args.InvokedItem is HamburgerMenuIconItem menuItem)
            {
                switch (menuItem.Tag)
                {
                    case "HomePage":
                        Tasks = DatabaseCommands.LoadTasksFromDb();
                        Responses = DatabaseCommands.LoadResponsesFromDb();
                        Users = DatabaseCommands.GetUsers();
                        MainFrame.Navigate(new HomePage(Tasks, this));
                        break;
                    case "SettingsPage":
                        MainFrame.Navigate(new Settings(this));
                        break;
                    case "CreateTask":
                        MainFrame.Navigate(new CreateTaskPage(this));
                        break;
                    case "ProfilePage":
                        MainFrame.Navigate(new UserProfilePage(CurrentUser));
                        break;
                    case "InBox":
                        Tasks = DatabaseCommands.LoadTasksFromDb();
                        Responses = DatabaseCommands.LoadResponsesFromDb();
                        if (IsCustomer) MainFrame.Navigate(new CustomerRequestsPage(Tasks.Where(a => a.CreatorId == CurrentUser.Id).ToList(), this));
                        else MainFrame.Navigate(new FreeRequestPage(Responses.Where(a => a.FreelancerId == CurrentUser.Id).ToList(), this));
                        break;
                }
            }
        }
    }
}
