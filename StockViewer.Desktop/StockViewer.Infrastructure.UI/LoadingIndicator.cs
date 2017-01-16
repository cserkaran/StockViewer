using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StockViewer.Infrastructure.UI
{
    /// <summary>
    /// Circular progress bar with option to display message below the progress bar.
    /// </summary>
    public class LoadingIndicator : Control
    {
        #region Dependency Properties Definition

        /// <summary>
        /// Identifies the FirstCircleBrush dependency property
        /// </summary>
        public static readonly DependencyProperty FirstCircleBrushProperty =
            DependencyProperty.Register(
                "FirstCircleBrush",
                typeof(Brush),
                typeof(LoadingIndicator),
                new UIPropertyMetadata(Brushes.Gray));

        /// <summary>
        /// Gets or sets the first circle brush.
        /// </summary>
        /// <value>
        /// The first circle brush.
        /// </value>
        public Brush FirstCircleBrush
        {
            get
            {
                return (Brush)GetValue(FirstCircleBrushProperty);
            }

            set
            {
                SetValue(FirstCircleBrushProperty, value);
            }
        }

        /// <summary>
        /// Identifies the SecondCircleBrush dependency property
        /// </summary>
        public static readonly DependencyProperty SecondCircleBrushProperty =
            DependencyProperty.Register(
                "SecondCircleBrush",
                typeof(Brush),
                typeof(LoadingIndicator),
                new UIPropertyMetadata(Brushes.LightGray));

        /// <summary>
        /// Gets or sets the second circle brush.
        /// </summary>
        /// <value>
        /// The second circle brush.
        /// </value>
        public Brush SecondCircleBrush
        {
            get
            {
                return (Brush)GetValue(SecondCircleBrushProperty);
            }

            set
            {
                SetValue(SecondCircleBrushProperty, value);
            }
        }

        /// <summary>
        /// Identifies the message property
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
           DependencyProperty.Register("Message",
                           typeof(string),
                           typeof(LoadingIndicator),
                           new UIPropertyMetadata(UI.Resources.ActionLoadingWithEllipsis));

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }

            set
            {
                SetValue(MessageProperty, value);
            }
        }

        /// <summary>
        /// Identifies the message foreground property
        /// </summary>
        public static readonly DependencyProperty MessageForegroundProperty =
           DependencyProperty.Register("MessageForeground",
                           typeof(Brush),
                           typeof(LoadingIndicator),
                           new UIPropertyMetadata(Brushes.White));

        /// <summary>
        /// Gets or sets the message foreground property.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public Brush MessageForeground
        {
            get
            {
                return (Brush)GetValue(MessageForegroundProperty);
            }

            set
            {
                SetValue(MessageForegroundProperty, value);
            }
        }

        #endregion
    }
}
