using System;
using System.Globalization;
using System.Windows.Data;

namespace FreelanceExchange_desktop.Helpers.Converters
{
    public class BoolToSelectedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected)
            {
                return isSelected ? "Выбрано" : "Не выбрано";
            }
            return "Не выбрано";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
