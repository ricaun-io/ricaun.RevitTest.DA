namespace Autodesk.Revit.UI
{
    using Autodesk.Revit.ApplicationServices;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Events;
    using System;
    using System.Reflection;

    /// <summary>
    /// Provides utility methods and properties for interacting with the Revit application.
    /// </summary>
    /// <remarks>Source: <a href="https://github.com/ricaun-io/ricaun.Revit.UI/blob/master/ricaun.Revit.UI/RevitApplication.cs"/> </remarks>
    public static class RevitApplication
    {
        /// <summary>
        /// Gets the current <see cref="UIApplication"/> instance.
        /// </summary>
        public static UIApplication UIApplication => new RibbonItemEventArgs().Application;
        /// <summary>
        /// Gets the current <see cref="UIControlledApplication"/> instance.
        /// </summary>
        public static UIControlledApplication UIControlledApplication => GetUIControlledApplication(UIApplication);
        /// <summary>
        /// Gets a value indicating whether the current context is within an add-in.
        /// </summary>
        public static bool IsInAddInContext => InAddInContext(UIApplication);

        #region Private
        /// <summary>
        /// Get <see cref="Autodesk.Revit.UI.UIControlledApplication"/> using the <paramref name="application"/>
        /// </summary>
        /// <param name="application">Revit UIControlledApplication</param>
        private static UIControlledApplication GetUIControlledApplication(UIApplication application)
        {
            var type = typeof(UIControlledApplication);

            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { application.GetType() }, null);

            return constructor?.Invoke(new object[] { application }) as UIControlledApplication;
        }
        private static bool InAddInContext(UIApplication uiapp)
        {
            // ActiveAddInId is only available when Revit is within an API context.
            return uiapp.ActiveAddInId is not null;
        }
        #endregion
    }

    /// <summary>
    /// RevitDBApplication
    /// </summary>
    public static class RevitDBApplication
    {
        /// <summary>
        /// Application
        /// </summary>
        public static Application Application => GetApplication();
        /// <summary>
        /// ControlledApplication
        /// </summary>
        public static ControlledApplication ControlledApplication => GetControlledApplication();

        #region Private
        private static Application GetApplication()
        {
            return new RibbonItemEventArgs().Application.Application;
        }

        private static ControlledApplication GetControlledApplication()
        {
            var type = typeof(ControlledApplication);

            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { Application.GetType() }, null);

            return constructor?.Invoke(new object[] { Application }) as ControlledApplication;
        }
        #endregion
    }
}