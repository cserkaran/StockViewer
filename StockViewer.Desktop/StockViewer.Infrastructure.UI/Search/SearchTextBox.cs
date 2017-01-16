using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StockViewer.Infrastructure.UI.Search
{
    /// <summary>
    /// Windows like glass search textbox control implementation.
    /// </summary>
    public class SearchTextBox : TextBox
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchTextBox"/> class.
        /// </summary>
        public SearchTextBox()
        {

        }

        #endregion

        #region Search Mode

        /// <summary>
        /// Indicates the mode of search e.g instant or delayed.
        /// <see cref="SearchMode"/>
        /// </summary>
        public static readonly DependencyProperty SearchModeProperty = DependencyProperty.Register(
            "SearchMode", typeof(SearchMode), typeof(SearchTextBox), new PropertyMetadata(SearchMode.Instant));

        /// <summary>
        /// Gets or sets the SearchModeProperty.
        /// </summary>
        public SearchMode SearchMode
        {
            get
            {
                return (SearchMode)GetValue(SearchModeProperty);
            }

            set
            {
                SetValue(SearchModeProperty, value);
            }
        }

        #endregion

        #region Default Search Text Label Properties

        /// <summary>
        /// Indicates the default search text label displayed when the control
        /// is loaded for the first time.
        /// </summary>
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
            "LabelText", typeof(string), typeof(SearchTextBox), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets LabelTextProperty
        /// </summary>
        public string LabelText
        {
            get
            {
                return (string)GetValue(LabelTextProperty);
            }

            set
            {
                SetValue(LabelTextProperty, value);
            }
        }

        /// <summary>
        /// Font Brush of the LabelText.
        /// </summary>
        public static readonly DependencyProperty LabelTextColorProperty = DependencyProperty.Register(
            "LabelTextColor", typeof(Brush), typeof(SearchTextBox), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Gets or sets LabelTextColorProperty
        /// </summary>
        public Brush LabelTextColor
        {
            get
            {
                return (Brush)GetValue(LabelTextColorProperty);
            }

            set
            {
                SetValue(LabelTextColorProperty, value);
            }
        }

        #endregion

        #region Search TextBox CornerRadius

        /// <summary>
        /// The corner radius property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(double), typeof(SearchTextBox), new PropertyMetadata(10.0));

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>
        /// The corner radius.
        /// </value>
        public double CornerRadius
        {
            get
            {
                return (double)GetValue(CornerRadiusProperty);
            }

            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

        #endregion

        #region Search/Cancel Search Events and Commands 

        /// <summary>
        /// Routed event to notify that user is typing in the textbox to search.
        /// </summary>
        public static readonly RoutedEvent SearchEvent = EventManager.RegisterRoutedEvent(
            "Search", RoutingStrategy.Bubble, typeof(EventHandler<SearchEventArgs>), typeof(SearchTextBox));

        /// <summary>
        /// Add or remove SearchEvent handler.
        /// </summary>
        public event EventHandler<SearchEventArgs> Search
        {
            add
            {
                AddHandler(SearchEvent, value);
            }

            remove
            {
                RemoveHandler(SearchEvent, value);
            }
        }

        /// <summary>
        /// Raise SearchEvent.
        /// </summary>
        private void RaiseSearchEvent(bool ignoreBindingDelay)
        {
            var args = new SearchEventArgs(SearchEvent) { IgnoreBindingDelay = ignoreBindingDelay, SearchText= this.Text };
            RaiseEvent(args);
        }

        /// <summary>
        /// Command fired when SearchEvent is raised.
        /// </summary>
        public static readonly DependencyProperty SearchCommand =
            EventBehaviorFactory.CreateCommandExecutionEventBehavior(SearchEvent, "SearchCommand", typeof(SearchTextBox));

        /// <summary>
        /// Set SearchCommand
        /// </summary>
        /// <param name="dependencyObject">Object to which the command is attached</param>
        /// <param name="value">ICommand value attached</param>
        public static void SetSearchCommand(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(SearchCommand, value);
        }

        /// <summary>
        /// Get SearchCommand
        /// </summary>
        /// <param name="dependencyObject">Object to which the command is attached</param>
        public static ICommand GetSearchCommand(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(SearchCommand) as ICommand;
        }

        /// <summary>
        /// Routed event to notify that user has cancelled search.
        /// </summary>
        public static readonly RoutedEvent CancelSearchEvent = EventManager.RegisterRoutedEvent(
           "CancelSearch", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchTextBox));

        /// <summary>
        /// Add or remove CancelSearchEvent handler.
        /// </summary>
        public event RoutedEventHandler CancelSearch
        {
            add
            {
                AddHandler(SearchEvent, value);
            }

            remove
            {
                RemoveHandler(SearchEvent, value);
            }
        }

        /// <summary>
        /// Raise CancelSearchEvent.
        /// </summary>
        private void RaiseCancelSearchEvent()
        {
            var args = new RoutedEventArgs(CancelSearchEvent);
            RaiseEvent(args);
        }

        /// <summary>
        /// Command fired when CancelSearchEvent is raised.
        /// </summary>
        public static readonly DependencyProperty CancelSearchCommand =
            EventBehaviorFactory.CreateCommandExecutionEventBehavior(CancelSearchEvent, "CancelSearchCommand", typeof(SearchTextBox));

        /// <summary>
        /// Get CancelSearchCommand
        /// </summary>
        /// <param name="dependencyObject">Object to which the command is attached</param>
        /// <param name="value">ICommand value attached</param>
        public static void SetCancelSearchCommand(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(CancelSearchCommand, value);
        }

        /// <summary>
        /// Get CancelSearchCommand
        /// </summary>
        /// <param name="dependencyObject">Object to which the command is attached</param>
        public static ICommand GetCancelSearchCommand(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(CancelSearchCommand) as ICommand;
        }

        #endregion

        #region Text Change Handling and firing of events

        /// <summary>
        /// Indicates if Search box has text or not.Read only property for HasText.
        /// </summary>
        private static readonly DependencyPropertyKey HasTextPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasText", typeof(bool), typeof(SearchTextBox), new PropertyMetadata());

        /// <summary>
        /// Indicates if Search box has text or not.
        /// </summary>
        public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets HasTextProperty
        /// </summary>
        public bool HasText
        {
            get
            {
                return (bool)GetValue(HasTextProperty);
            }

            private set
            {
                SetValue(HasTextPropertyKey, value);
            }
        }

        /// <summary>
        /// Override TextChange from base class.
        /// Fire appropriate Search events based on HasText and SearchMode.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            HasText = Text.Length != 0;
            if (this.SearchMode != SearchMode.Instant)
            {
                return;
            }

            if (this.HasText)
            {
                this.RaiseSearchEvent(false);
            }
            else
            {
                this.RaiseCancelSearchEvent();
            }
        }

        /// <summary>
        /// Readonly property  key for IsMouseLeftButtonDownProperty
        /// </summary>
        private static readonly DependencyPropertyKey IsMouseLeftButtonDownPropertyKey =
            DependencyProperty.RegisterReadOnly("IsMouseLeftButtonDown",
                typeof(bool), typeof(SearchTextBox), new PropertyMetadata());

        /// <summary>
        /// Indicates if Mouseleftbutton is dowm on the control or not.
        /// </summary>
        private static readonly DependencyProperty IsMouseLeftButtonDownProperty =
            IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets IsMouseLeftButtonDownProperty
        /// </summary>
        public bool IsMouseLeftButtonDown
        {
            get
            {
                return (bool)GetValue(IsMouseLeftButtonDownProperty);
            }

            private set
            {
                SetValue(IsMouseLeftButtonDownPropertyKey, value);
            }
        }

        /// <summary>
        /// Called before control is loaded.
        /// Find the required controls e.g icon and close button and register for 
        /// appropriate event handlers.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var iconBorder = this.GetTemplateChild("PART_SearchIconBorder") as Border;
            if (iconBorder != null)
            {
                iconBorder.MouseLeftButtonDown += this.IconBorderMouseLeftButtonDown;
                iconBorder.MouseLeftButtonUp += this.IconBorderMouseLeftButtonUp;
                iconBorder.MouseLeave += this.IconBorderMouseLeave;
            }

            var closeButton = this.GetTemplateChild("PART_Close") as Button;

            if (closeButton != null)
            {
                closeButton.Click += this.CloseButtonClick;
            }
        }

        /// <summary>
        /// Close button click event handler.
        /// Clear the search text.
        /// </summary>
        /// <param name="sender">sender object.</param>
        /// <param name="e">RoutedEvent arguments</param>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void IconBorderMouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            IsMouseLeftButtonDown = true;
        }

        private void IconBorderMouseLeftButtonUp(object obj, MouseButtonEventArgs e)
        {
            if (!IsMouseLeftButtonDown)
            {
                return;
            }

            if (HasText)
            {
                // Simulate enter key press to start searching..
                RaiseSearchEvent(true);
            }

            IsMouseLeftButtonDown = false;
        }

        /// <summary>
        /// Occur when mouse leaves icon border.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void IconBorderMouseLeave(object obj, MouseEventArgs e)
        {
            IsMouseLeftButtonDown = false;
        }

        /// <summary>
        /// OnKeyDown override. Check the keys and raise appropriate events or do some operation e.g 
        /// on enter raise SearchEvent and on escape raise clear the text.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    {
                        this.Text = string.Empty;
                        PresentationHelper.UpdateDelayBindingSource(this, TextProperty);
                    }

                    e.Handled = true;
                    break;

                case Key.Enter:
                    RaiseSearchEvent(true);
                    PresentationHelper.UpdateDelayBindingSource(this, TextProperty);
                    e.Handled = true;
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #endregion
    }
}
