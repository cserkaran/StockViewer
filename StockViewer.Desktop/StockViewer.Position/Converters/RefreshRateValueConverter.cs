using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using StockViewer.Infrastructure;
using StockViewer.Position.PositionSummary;

namespace StockViewer.Position.Converters
{
    /// <summary>
    /// Convert slider value to refresh rate display.
    /// </summary>
    /// <seealso cref="Infrastructure.SingletonBase{RefreshRateValueConverter}" />
    /// <seealso cref="IValueConverter" />
    public class RefreshRateValueConverter : SingletonBase<RefreshRateValueConverter> , IValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.Parse(value.ToString()) >= ObservableStockPosition.StopRefreshLimit)
            {
                return $" : {Resources.Resources.RefreshOff}";
            }

            return $" : {value} s";
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
