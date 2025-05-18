using FreelanceExchange_desktop.Data;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for CreateTaskPage.xaml
    /// </summary>
    public partial class CreateTaskPage : Page
    {
        public ICommand CreateTaskCommand { get; set; }
        public Task NewTask { get; set; } = new Task();
        private MainWindow _mainWindow;
        public CreateTaskPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = this;
            CreateTaskCommand = new DelegateCommand(CreateTask);
        }

        private void CreateTask(object obj)
        {
            NewTask.CreatedAt = DateTime.Now;
            NewTask.CreatorId = _mainWindow.CurrentUser.Id;
            NewTask.StatusId = 1;
            _mainWindow.Tasks.Add(NewTask);
            DatabaseCommands.AddTaskToDatabase(NewTask);
            _mainWindow.MainFrame.Navigate(new SuccesfulyCreateTask());
        }

        public bool IsTaskValid(Task newTask)
        {
            return !string.IsNullOrWhiteSpace(newTask.Title) &&
                   !string.IsNullOrWhiteSpace(newTask.Description) &&
                   newTask.Budget > 0 &&
                   newTask.StatusId > 0;
        }

    }
}
