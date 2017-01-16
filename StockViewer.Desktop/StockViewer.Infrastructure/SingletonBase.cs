using System;

namespace StockViewer.Infrastructure
{
    /// <summary>
    /// Abstract base class for singleton creation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBase<T> where T:class,new()
    {
        #region Members

        /// <summary>
        /// Static instance. Needs to use lambda expression
        /// to construct an instance (since constructor is private).
        /// </summary>
        private static readonly Lazy<T> _instance = new Lazy<T>(() => CreateInstanceOfT());

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance of this singleton.
        /// </summary>
        public static T Instance { get { return _instance.Value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of T via reflection since T's constructor is expected to be private.
        /// </summary>
        /// <returns></returns>
        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }

        #endregion
    }
}
