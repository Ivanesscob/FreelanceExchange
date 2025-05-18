using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages.Auth
{
    /// <summary>
    /// Interaction logic for Signin.xaml
    /// </summary>
    public partial class Signin : Page
    {
        private Frame _frame;

        public ICommand SignInCommand { get; }
        public ICommand NavigateToRegCommand { get; }
        public Signin(Frame a)
        {
            InitializeComponent();
            _frame = a;
            DataContext = this;
            SignInCommand = new DelegateCommand(SignIn);
            NavigateToRegCommand = new DelegateCommand(NavigateToReg);
        }

        private void SignIn(object obj)
        {
            _frame.Visibility = Visibility.Collapsed;
        }

        private void NavigateToReg(object obj)
        {
            _frame.Navigate(new Registration(_frame));
        }
    }
}
