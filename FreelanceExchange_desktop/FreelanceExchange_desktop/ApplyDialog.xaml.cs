using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceExchange_desktop
{
    /// <summary>
    /// Interaction logic for ApplyDialog.xaml
    /// </summary>
    public partial class ApplyDialog : MetroWindow, INotifyPropertyChanged
    {
        public bool IsYes;
        public ICommand YesCommand { get; set; }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public ApplyDialog()
        {
            InitializeComponent();
            DataContext = this;
            YesCommand = new DelegateCommand(Yes);
        }

        private void Yes(object obj)
        {
            IsYes = true;
            Close();
        }

        private void PriceBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(((TextBox)sender).Text + e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            return Regex.IsMatch(text, @"^\d*([.,]\d{0,2})?$");
        }

    }
}
