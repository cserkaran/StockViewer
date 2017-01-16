using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace StockViewer.Infrastructure
{
    /// <summary>
    /// The window
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// The GWL style from winuser.h
        /// </summary>
        private const int GWL_STYLE = -16,
                          WS_MAXIMIZEBOX = 0x10000,
                          WS_MINIMIZEBOX = 0x20000;

        /// <summary>
        /// Gets the window long.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        extern private static int GetWindowLong(IntPtr hwnd, int index);

        /// <summary>
        /// Sets the window long.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        extern private static int SetWindowLong(IntPtr hwnd, int index, int value);

        /// <summary>
        /// Hides the minimize button.
        /// </summary>
        /// <param name="window">The window.</param>
        public static void HideMinimizeButton(this Window window)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
            var currentStyle = GetWindowLong(hwnd, GWL_STYLE);

            SetWindowLong(hwnd, GWL_STYLE, (currentStyle & ~WS_MINIMIZEBOX));
        }
    }
}
