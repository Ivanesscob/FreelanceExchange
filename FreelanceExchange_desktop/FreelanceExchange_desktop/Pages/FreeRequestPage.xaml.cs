using FreelanceExchange_desktop.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for FreeRequestPage.xaml
    /// </summary>
    public partial class FreeRequestPage : Page
    {
        public List<Response> UserResponses { get; set; }
        public Response SelectedResponse { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private MainWindow _mainWindow;

        public FreeRequestPage(List<Response> userResponses, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            _mainWindow = mainWindow;
            UserResponses = userResponses;
            DeleteCommand = new DelegateCommand(Delete);
        }

        public void Edit(object obj, MouseButtonEventArgs e)
        {
            
        }

        private void Delete(object obj)
        {
            if (MessageBox.Show("Подтверждение удаления", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseCommands.DeleteResponse(SelectedResponse);
                _mainWindow.Responses.Remove(SelectedResponse);
                _mainWindow.MainFrame.Navigate(new FreeRequestPage(_mainWindow.Responses.Where(a => a.FreelancerId == _mainWindow.CurrentUser.Id).ToList(), _mainWindow));
            }
        }
    }
}
