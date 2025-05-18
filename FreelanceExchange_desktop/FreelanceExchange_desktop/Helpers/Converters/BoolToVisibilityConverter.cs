using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FreelanceExchange_desktop.Helpers.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        // true => Visible, false => Collapsed
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Collapsed;
        }

        // Обратное преобразование (Visibility => bool)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
                return visibility == Visibility.Visible;
            return false;
        }
    }
}
