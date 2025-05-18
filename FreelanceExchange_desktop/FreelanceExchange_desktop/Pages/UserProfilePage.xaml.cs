using FreelanceExchange_desktop.Data;
using System.Windows.Controls;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        public User CurrentUser { get; set; }
        public UserProfilePage(User currentUser)
        {
            InitializeComponent();
            DataContext = this;
            CurrentUser = currentUser;
        }
    }
}
