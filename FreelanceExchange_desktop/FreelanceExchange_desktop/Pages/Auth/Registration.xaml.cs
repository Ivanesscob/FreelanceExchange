using FreelanceExchange_desktop.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages.Auth
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page, INotifyPropertyChanged
    {
        private Frame _frame;
        private MainWindow _mainWindow;
        public User NewUser { get; set; } = new User();
        public ICommand RegCommand { get; }
        public ICommand NavigateToSignInCommand { get; }
        public List<string> Roles { get; set; } = new List<string> { "freelancer", "customer" };

        private string selectedRole;
        public string SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Registration(Frame frame, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            _frame = frame;
            RegCommand = new DelegateCommand(Register);
            NavigateToSignInCommand = new DelegateCommand(NavigateToSignIn);
            this._mainWindow = mainWindow;
        }

        private void Register(object obj)
        {
            if (DatabaseCommands.IsUserUnique(NewUser) && !HasEmptyFields(NewUser))
            {
                DatabaseCommands.AddUser(NewUser, SelectedRole);
                _frame.Navigate(new Signin(_frame, _mainWindow));
            }
            else
            {
                MessageBox.Show("1. Заполните все поля\n" +
                    "2. Такая почта или логин зарегестрированы", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool HasEmptyFields(User user)
        {
            return string.IsNullOrWhiteSpace(user.Email) ||
                   string.IsNullOrWhiteSpace(user.FirstName) ||
                   string.IsNullOrWhiteSpace(user.LastName) ||
                   string.IsNullOrWhiteSpace(user.Username) ||
                   string.IsNullOrWhiteSpace(user.Password);
        }

        private void NavigateToSignIn(object obj)
        {
            _frame.Navigate(new Signin(_frame, _mainWindow));
        }
    }
}
