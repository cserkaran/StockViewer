using System;
using System.Globalization;
using System.Windows.Data;

namespace StockViewer.Infrastructure.Converters
{
    /// <summary>
    /// Converter decimal value to currency.
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class CurrencyConverter : SingletonBase<CurrencyConverter>, IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var result = value as decimal?;
            
            if (result == null)
                result = 0;

            return string.Format(CultureInfo.CurrentUICulture, "{0:C}", result.Value);
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}