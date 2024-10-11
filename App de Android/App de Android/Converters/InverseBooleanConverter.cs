using System;
using System.Globalization;
using Xamarin.Forms;

namespace App_de_Android.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Invert the boolean value
            if (value is bool booleanValue)
            {
                return !booleanValue;
            }
            return false; // Default to false if value is not a boolean
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Invert back the boolean value (optional)
            if (value is bool booleanValue)
            {
                return !booleanValue;
            }
            return false; // Default to false if value is not a boolean
        }
    }
}
