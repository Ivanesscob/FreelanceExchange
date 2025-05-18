using FreelanceExchange_desktop.Data;
using System.Collections.Generic;
using System.Windows.Controls;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public List<Task> Tasks { get; set; }
        public HomePage(List<Task> tasks)
        {
            InitializeComponent();
            DataContext = this;
            Tasks = tasks;
        }
    }
}