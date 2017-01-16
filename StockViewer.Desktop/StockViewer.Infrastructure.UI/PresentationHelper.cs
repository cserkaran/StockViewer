using System.Windows;
using System.Windows.Data;

namespace StockViewer.Infrastructure.UI
{
    /// <summary>
    /// WPF presentation class member helpers.Utility methods to do some general tasks 
    /// e.g search for templates in resources of Framework element  
    /// </summary>
    public static class PresentationHelper
    {
        /// <summary>
        /// Updates the delay binding source.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="dependencyProperty">The dependency property.</param>
        public static void UpdateDelayBindingSource(FrameworkElement element, DependencyProperty dependencyProperty)
        {
            var binding = BindingOperations.GetBinding(element, dependencyProperty);

            // if binding has a delay set and mode is not one time or one way..update the source.
            if (binding == null || binding.Delay <= 0 || binding.Mode == BindingMode.OneTime
                || binding.Mode == BindingMode.OneWay)
            {
                return;
            }

            var be = element.GetBindingExpression(dependencyProperty);
            if (be != null)
            {
                be.UpdateSource();
            }
        }
    }
}
