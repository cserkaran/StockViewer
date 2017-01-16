using System;
using System.Windows;
using System.Windows.Input;

namespace StockViewer.Infrastructure.UI
{

    /// <summary>
    /// Class for hooking up commands to events in WPF.Taken from http://blog.functionalfun.net/2008/09/hooking-up-commands-to-events-in-wpf.html
    /// </summary>
    public static class EventBehaviorFactory
    {
        #region Create Command from Event 

        /// <summary>
        /// Creates the command execution event behavior.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        public static DependencyProperty CreateCommandExecutionEventBehavior(RoutedEvent routedEvent, string propertyName, Type ownerType)
        {
            var property = DependencyProperty.RegisterAttached(
                propertyName,
                typeof(ICommand),
                ownerType,
                new PropertyMetadata(null, new ExecuteCommandOnRoutedEventBehavior(routedEvent).PropertyChangedHandler));

            return property;
        }

        #endregion

        #region Nested Types for Executing the command.

        /// <summary>
        /// Responsible for executing the command when event occurs.
        /// </summary>
        private abstract class ExecuteCommandBehavior
        {
            #region Fields

            /// <summary>
            /// The property
            /// </summary>
            private DependencyProperty property;

            #endregion

            #region Event Handling

            /// <summary>
            /// Adjusts the event handlers.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="oldValue">The old value.</param>
            /// <param name="newValue">The new value.</param>
            protected abstract void AdjustEventHandlers(DependencyObject sender, object oldValue, object newValue);

            /// <summary>
            /// Handles the event.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
            protected void HandleEvent(object sender, EventArgs e)
            {
                var dp = sender as DependencyObject;
                if (dp == null)
                {
                    return;
                }

                var command = dp.GetValue(property) as ICommand;

                if (command == null)
                {
                    return;
                }

                if (command.CanExecute(e))
                {
                    command.Execute(e);
                }
            }

            #endregion

            #region Property Change Handling

            /// <summary>
            /// Listens for a change in the DependencyProperty that we are assigned to, and
            /// adjusts the EventHandlers accordingly
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void PropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
            {
                // the first time the property changes,
                // make a note of which property we are supposed
                // to be watching
                if (property == null)
                {
                    property = e.Property;
                }

                var oldValue = e.OldValue;
                var newValue = e.NewValue;

                AdjustEventHandlers(sender, oldValue, newValue);
            }

            #endregion
        }

        /// <summary>
        /// An internal class to handle listening for an event and executing a command,
        /// when a Command is assigned to a particular DependencyProperty
        /// </summary>
        private class ExecuteCommandOnRoutedEventBehavior : ExecuteCommandBehavior
        {
            #region Properties 

            /// <summary>
            /// The routed event.
            /// </summary>
            private readonly RoutedEvent routed;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="ExecuteCommandOnRoutedEventBehavior"/> class.
            /// </summary>
            /// <param name="routedEvent">The routed event.</param>
            public ExecuteCommandOnRoutedEventBehavior(RoutedEvent routedEvent)
            {
                routed = routedEvent;
            }

            #endregion

            #region Event Handling

            /// <summary>
            /// Handles attaching or Detaching Event handlers when a Command is assigned or unassigned
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="oldValue"></param>
            /// <param name="newValue"></param>
            protected override void AdjustEventHandlers(DependencyObject sender, object oldValue, object newValue)
            {
                var element = sender as UIElement;

                if (element == null)
                {
                    return;
                }

                if (oldValue != null)
                {
                    element.RemoveHandler(routed, new RoutedEventHandler(EventHandler));
                }

                if (newValue != null)
                {
                    element.AddHandler(routed, new RoutedEventHandler(EventHandler));
                }
            }

            /// <summary>
            /// The event handler..
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
            private void EventHandler(object sender, RoutedEventArgs e)
            {
                HandleEvent(sender, e);
            }

            #endregion
        }

        #endregion
    }
}
