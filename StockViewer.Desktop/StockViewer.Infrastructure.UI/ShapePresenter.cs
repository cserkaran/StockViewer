using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace StockViewer.Infrastructure.UI
{
    /// <summary>
    /// Control to present shapes i.e paths,icons etc.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ContentControl" />
    public class ShapePresenter : ContentControl
    {
        /// <summary>
        /// Called when the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property changes.
        /// </summary>
        /// <param name="oldContent">The old value of the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property.</param>
        /// <param name="newContent">The new value of the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property.</param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            var pathContent = newContent as Shape;
            if (pathContent != null && Tag == null)
            {
                pathContent.SetBinding(Shape.FillProperty, new Binding("Foreground") { Source = this });
            }
        }
    }
}
