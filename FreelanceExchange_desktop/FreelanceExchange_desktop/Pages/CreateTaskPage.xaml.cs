using FreelanceExchange_desktop.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ICommand UploadImageCommand { get; set; }
        public Task NewTask { get; set; } = new Task();
        private MainWindow _mainWindow;
        public CreateTaskPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = this;
            CreateTaskCommand = new DelegateCommand(CreateTask);
            UploadImageCommand = new DelegateCommand(UploadImage);
        }
        private void UploadImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                string originalFileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                string extension = System.IO.Path.GetExtension(openFileDialog.FileName);

                string uniqueFileName = $"{originalFileName}_{Guid.NewGuid().ToString().Substring(0, 8)}{extension}";

                string projectPicsPath = @"C:\FreelanceExchange\FreelanceExchange_desktop\FreelanceExchange_desktop\Pics\UserPics";
                string destinationPath = System.IO.Path.Combine(projectPicsPath, uniqueFileName);

                File.Copy(openFileDialog.FileName, destinationPath);

                NewTask.Image = uniqueFileName;
            }

        }

        private void CreateTask(object obj)
        {
            if (IsTaskValid(NewTask))
            {
                NewTask.CreatedAt = DateTime.Now;
                NewTask.CreatorId = _mainWindow.CurrentUser.Id;
                NewTask.StatusId = 1;
                _mainWindow.Tasks.Add(NewTask);
                DatabaseCommands.AddTaskToDatabase(NewTask);
                _mainWindow.MainFrame.Navigate(new SuccesfulyCreateTask());
            }
        }

        public bool IsTaskValid(Task newTask)
        {
            return !string.IsNullOrWhiteSpace(newTask.Title) &&
                   !string.IsNullOrWhiteSpace(newTask.Description) &&
                   newTask.Budget > 0;
        }

    }
}
