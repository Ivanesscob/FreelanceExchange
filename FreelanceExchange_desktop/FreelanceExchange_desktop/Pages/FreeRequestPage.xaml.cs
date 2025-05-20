using FreelanceExchange_desktop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for FreeRequestPage.xaml
    /// </summary>
    public partial class FreeRequestPage : Page
    {
        public List<Response> UserTasks { get; set; }
        public Response SelectedTask { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private MainWindow _mainWindow;
        public FreeRequestPage(List<Response> userResponses, MainWindow mainWindow)
        {
            InitializeComponent();
        }
    }
}
