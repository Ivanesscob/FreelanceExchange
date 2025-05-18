using FreelanceExchange_desktop.Data;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace FreelanceExchange_desktop
{
    public partial class MainWindow : MetroWindow
    {
        public List<User> Users;
        public User CurrentUser;
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

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}

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
        }

        private void HamburgerMenu_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            if (args.InvokedItem is HamburgerMenuIconItem menuItem)
            {
                switch (menuItem.Tag)
                {
                    case "HomePage":
                        break;
                    case "SettingsPage":
                        break;
                }
            }
        }
    }
}
