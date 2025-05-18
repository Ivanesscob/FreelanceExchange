using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages.Auth
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private Frame _frame;
        public ICommand RegCommand { get; }
        public ICommand NavigateToSignInCommand { get; }
        public Registration(Frame frame)
        {
            InitializeComponent();
            DataContext = this;
            _frame = frame;
            RegCommand = new DelegateCommand(Register);
            NavigateToSignInCommand = new DelegateCommand(NavigateToSignIn);
        }

        private void Register(object obj)
        {
            _frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void NavigateToSignIn(object obj)
        {
            _frame.Navigate(new Signin(_frame));
        }
    }
}
