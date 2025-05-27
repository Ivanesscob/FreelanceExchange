using FreelanceExchange_desktop.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for ResponsesForCusomer.xaml
    /// </summary>
    public partial class ResponsesForCusomer : Page
    {
        private MainWindow _mainWindow;
        public List<Response> Responses { get; set; }
        public ICommand ApplyCommand { get; set; }
        public Response SelectedResponse { get; set; }
        private Task SelectedTask;
        public ResponsesForCusomer(MainWindow mainWindow, Task selectedTask)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = this;
            SelectedTask = selectedTask;
            SetValue(selectedTask);
            ApplyCommand = new DelegateCommand(Apply, _ => SelectedResponse != null);
        }

        private void SetValue(Task st)
        {
            Responses = _mainWindow.Responses.Where(_ => _.TaskId == st.Id).ToList();
        }

        private void Apply(object obj)
        {
            if (DatabaseCommands.SelectResponseForTask(Responses, SelectedResponse, SelectedTask)) MessageBox.Show("Польлзователь выбран", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
