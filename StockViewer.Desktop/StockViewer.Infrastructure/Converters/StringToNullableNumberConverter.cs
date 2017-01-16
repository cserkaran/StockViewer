using System;
using System.Globalization;
using System.Windows.Data;

namespace StockViewer.Infrastructure.Converters
{
    /// <summary>
    /// Convert string value to a nullable number.
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class StringToNullableNumberConverter : SingletonBase<StringToNullableNumberConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;
            if (stringValue != null)
            {
                if (targetType == typeof(int?))
                {
                    int result;
                    if (int.TryParse(stringValue, out result))
                        return result;
                    
                    return null;
                }

                if (targetType == typeof(decimal?))
                {
                    decimal result;
                    if (decimal.TryParse(stringValue, out result))
                        return result;

                    return null;
                }
            }

            return value;
        }
    }
}