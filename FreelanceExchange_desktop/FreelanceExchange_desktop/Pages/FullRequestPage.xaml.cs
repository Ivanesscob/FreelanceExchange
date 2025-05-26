using FreelanceExchange_desktop.Data;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for FullRequestPage.xaml
    /// </summary>
    public partial class FullRequestPage : Page, INotifyPropertyChanged
    {
        public Task SelectedTask { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand EnableCommand { get; set; }

        private bool _isFreelancer;
        private MainWindow _mainWindow;
        public bool IsFreelancer
        {
            get => _isFreelancer;
            set
            {
                if (_isFreelancer != value)
                {
                    _isFreelancer = value;
                    OnPropertyChanged(nameof(IsFreelancer));
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FullRequestPage(Task selectedTask, MainWindow M)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTask = selectedTask;
            IsFreelancer = M.Isfreelancer;
            _mainWindow = M;
            EnableCommand = new DelegateCommand(Enable);
        }

        private void Enable(object obj)
        {
            ApplyDialog ad = new ApplyDialog();
             ad.ShowDialog();

            if (ad.IsYes == true)
            {
                Response response = new Response
                {
                    TaskId = SelectedTask.Id,
                    FreelancerId = _mainWindow.CurrentUser.Id,
                    Message = ad.Message,
                    ProposedPrice = ad.Price,
                    CreatedAt = DateTime.Now,
                    IsSelected = false
                };
                try
                {
                    DatabaseCommands.InsertResponse(response);
                }
                catch
                {
                    MessageBox.Show("Нелья подать заявку 2 раза", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
