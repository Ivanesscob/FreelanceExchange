using MahApps.Metro.Controls;

namespace FreelanceExchange_desktop
{
    public partial class MainWindow : MetroWindow
    {
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
            OverlayFrame.Navigate(new Pages.Auth.Signin(OverlayFrame));
            HamburgerMenu.ItemInvoked += HamburgerMenu_ItemInvoked;
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
