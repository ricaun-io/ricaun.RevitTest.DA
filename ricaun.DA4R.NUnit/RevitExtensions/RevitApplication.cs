namespace Autodesk.Revit.UI
{
    using Autodesk.Revit.ApplicationServices;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Events;
    using System;
    using System.Reflection;

    /// <summary>
    /// RevitApplication
    /// </summary>
    public static class RevitApplication
    {
        /// <summary>
        /// UIApplication
        /// </summary>
        public static UIApplication UIApplication { get; } = new RibbonItemEventArgs().Application;
        /// <summary>
        /// UIControlledApplication
        /// </summary>
        public static UIControlledApplication UIControlledApplication { get; } = GetUIControlledApplication();
        #region Private
        private static UIControlledApplication GetUIControlledApplication()
        {
            var type = typeof(UIControlledApplication);

            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { UIApplication.GetType() }, null);

            return constructor?.Invoke(new object[] { UIApplication }) as UIControlledApplication;
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
        public static Application Application { get; } = GetApplication();
        /// <summary>
        /// ControlledApplication
        /// </summary>
        public static ControlledApplication ControlledApplication { get; } = GetControlledApplication();

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