namespace FreelanceExchange_desktop.Helpers.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = value as string;
            if (string.IsNullOrEmpty(fileName))
                return null;

            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pics", "UserPics", fileName);

            if (!System.IO.File.Exists(path))
                return null;

            return new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
