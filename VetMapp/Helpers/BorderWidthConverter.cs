using System;
using Windows.UI.Xaml.Data;

namespace VetMapp.Helpers
{
    public class BorderWidthConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (double)value / 2 + 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}