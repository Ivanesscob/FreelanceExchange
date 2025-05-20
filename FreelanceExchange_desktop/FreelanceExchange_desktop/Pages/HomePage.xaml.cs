using FreelanceExchange_desktop.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public HomePage(ObservableCollection<Task> tasks)
        {
            InitializeComponent();
            DataContext = this;
            Tasks = tasks;
        }
    }
}