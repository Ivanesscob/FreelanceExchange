using FreelanceExchange_desktop.Data;
using FreelanceExchange_desktop.Pages;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace FreelanceExchange_desktop
{
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        public List<User> Users;
        public User CurrentUser;
        public List<Task> Tasks;

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
            get => isCustomer;
            set
            {
                if (isCustomer != value)
                {
                    isCustomer = value;
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

            LoadData();
        }

        private void LoadData()
        {
            Users = DatabaseCommands.GetUsers();
            Tasks = DatabaseCommands.LoadTasksFromDb();
        }

        private void HamburgerMenu_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            if (args.InvokedItem is HamburgerMenuIconItem menuItem)
            {
                switch (menuItem.Tag)
                {
                    case "HomePage":
                        MainFrame.Navigate(new HomePage(Tasks));
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
                }
            }
        }
    }
}
