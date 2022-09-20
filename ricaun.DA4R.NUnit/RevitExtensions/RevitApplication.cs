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
        /// <summary>
        /// IsInContext
        /// </summary>
        public static bool IsInContext => IsInRevitContext(UIApplication);

        #region Private
        private static UIControlledApplication GetUIControlledApplication()
        {
            var type = typeof(UIControlledApplication);

            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { UIApplication.GetType() }, null);

            return constructor?.Invoke(new object[] { UIApplication }) as UIControlledApplication;
        }
        private static bool IsInRevitContext(UIApplication uiapp)
        {
            try
            {
                uiapp.Idling += UiApplication_Idling;
                uiapp.Idling -= UiApplication_Idling;
                return true;
            }
            catch { }
            // Invalid call to Revit API! Revit is currently not within an API context.
            return false;
        }
        private static void UiApplication_Idling(object sender, IdlingEventArgs e) { }
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