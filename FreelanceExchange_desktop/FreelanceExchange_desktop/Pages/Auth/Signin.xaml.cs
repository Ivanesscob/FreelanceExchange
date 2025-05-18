using System.Linq;
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
        private MainWindow _mainWindow;
        public ICommand SignInCommand { get; }
        public ICommand NavigateToRegCommand { get; }
        public string LoginText { get; set; }
        public string PasswordText { get; set; }
        public Signin(Frame a, MainWindow mainWindow)
        {
            InitializeComponent();
            _frame = a;
            DataContext = this;
            SignInCommand = new DelegateCommand(SignIn);
            NavigateToRegCommand = new DelegateCommand(NavigateToReg);
            this._mainWindow = mainWindow;
        }

        private void SignIn(object obj)
        {

            var matchedUser = _mainWindow.Users.FirstOrDefault(u => u.Username == LoginText && u.Password == PasswordText);

            if (matchedUser != null)
            {
                _mainWindow.CurrentUser = matchedUser;
                if (_mainWindow.CurrentUser.Roles.First() == "customer") _mainWindow.IsCustomer = true;
                _frame.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateToReg(object obj)
        {
            _frame.Navigate(new Registration(_frame, _mainWindow));
        }
    }
}
