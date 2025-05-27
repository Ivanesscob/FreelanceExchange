using MahApps.Metro.Controls;
using FreelanceExchange_desktop.Data;
using System.Windows;
using System.Windows.Controls;
using ZstdSharp.Unsafe;

namespace FreelanceExchange_desktop.Pages
{
    /// <summary>
    /// Interaction logic for ProxyWindow.xaml
    /// </summary>
    public partial class ProxyWindow : MetroWindow
    {
        public Frame MainFrame { get; set; } = new Frame();
        public ProxyWindow(int a, MainWindow mainWindow,Task selectedTask = null)
        {
            InitializeComponent();
            DataContext = this;
            SetContent(a, selectedTask, mainWindow);
        }

        private void SetContent(int a, Task selectedTask, MainWindow M)
        {
            switch (a)
            {
                case 0:
                    MainFrame.Navigate(new ResponsesForCusomer(M, selectedTask));
                    break;
                case 1:
                    MainFrame.Navigate(new FullRequestPage(selectedTask, M));
                    break;
                default:
                    MessageBox.Show("Ошибка");
                    break;
            }
        }
    }
}
