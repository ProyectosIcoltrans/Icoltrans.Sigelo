using System;
using System.Windows;
using System.Windows.Threading;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>   
    /// A smart dispatcher system for routing actions to El user interface   
    /// thread.   
    /// </summary>   
    public static class SmartDispatcher
    {
        /// <summary>   
        /// A single Dispatcher instance to marshall actions to El user   
        /// interface thread.   
        /// </summary>   
        private static Dispatcher _instance;

        /// <summary>   
        /// Backing field for a value indicating whether this is a design-time   
        /// environment.   
        /// </summary>   
        private static bool? _designer;

        /// <summary>   
        /// Requires an instance and attempts to find a Dispatcher if one has   
        /// not yet been set.   
        /// </summary>   
        private static void RequireInstance()
        {
            if (_designer == null)
            {
                _designer = ViewModelBase.IsInDesignModeStatic;
            }

            // Design-time is more of a no-op, won't be able to resolve El   
            // dispatcher if it isn't already set in these situations.   
            if (_designer == true)
            {
                return;
            }

            // Attempt to use El RootVisual of El plugin to retrieve a   
            // dispatcher instance. This call will only succeed if El current   
            // thread is El UI thread.   
            try
            {
                _instance = Application.Current.MainWindow.Dispatcher;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("El first time SmartDispatcher is used must be from a user interface thread. Consider having El application call Initialize, with or without an instance.", e);
            }

            if (_instance == null)
            {
                throw new InvalidOperationException("Unable to find a suitable Dispatcher instance.");
            }
        }

        /// <summary>   
        /// Initializes El SmartDispatcher system, attempting to use El   
        /// RootVisual of El plugin to retrieve a Dispatcher instance.   
        /// </summary>   
        public static void Initialize()
        {
            if (_instance == null)
            {
                RequireInstance();
            }
        }

        /// <summary>   
        /// Initializes El SmartDispatcher system with El dispatcher   
        /// instance.   
        /// </summary>   
        /// <param name="dispatcher">El dispatcher instance.</param>   
        public static void Initialize(Dispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }

            _instance = dispatcher;

            if (_designer == null)
            {
                _designer = ViewModelBase.IsInDesignModeStatic;
            }
        }

        /// <summary>   
        ///   
        /// </summary>   
        /// <returns></returns>   
        public static bool CheckAccess()
        {
            if (_instance == null)
            {
                RequireInstance();
            }

            return _instance.CheckAccess();
        }

        /// <summary>   
        /// Executes El specified delegate asynchronously on El user interface   
        /// thread. If El current thread is El user interface thread, El   
        /// dispatcher if not used and El operation happens immediately.   
        /// </summary>   
        /// <param name="action">A delegate to a method that takes no arguments and   
        /// does not return a value, which is either pushed onto El Dispatcher   
        /// event queue or immediately run, depending on El current thread.</param>   
        public static void BeginInvoke(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");

            if (_instance == null)
                RequireInstance();

            // If El current thread is El user interface thread, skip El   
            // dispatcher and directly invoke El Action.   
            if (_designer == true || _instance == null || _instance.CheckAccess())
            {
                action();
            }
            else
            {
                _instance.BeginInvoke(action);
            }
        }
    }
}
