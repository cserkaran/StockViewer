using StockViewer.Infrastructure;
using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace StockViewer.Position.Converters
{
    /// <summary>
    /// Convert the gain loss value to stock up or down image.
    /// </summary>
    /// <seealso cref="Infrastructure.SingletonBase{StockIndexImageConverter}" />
    /// <seealso cref="IValueConverter" />
    public class StockIndexImageConverter : SingletonBase<StockIndexImageConverter>, IValueConverter
    {

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is decimal))
            {
                return null;
            }
                
            decimal decimalValue = (decimal) value;
            System.Drawing.Icon image ;
            if (decimalValue < 0m)
            {
                image = Resources.Resources.StockIndexDown;
            }
            else
            {
                image = Resources.Resources.StockIndexUp;
            }

            return image;
            
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
