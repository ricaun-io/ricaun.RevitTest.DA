using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.DA4R.NUnit.Services;
using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace ricaun.DA4R.NUnit.Revit
{
    public class App : IExternalDBApplication
    {
        IDisposable designAutomation;
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(this.GetType().Assembly.FullName);
            Console.WriteLine($"Framework: {typeof(App).Assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName}");
            Console.WriteLine("--------------------------------------------------");

            designAutomation = new DesignAutomationLoadVersion<DesignAutomationController>();
            return ExternalDBApplicationResult.Succeeded;
        }

        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            designAutomation?.Dispose();
            Console.WriteLine("--------------------------------------------------");
            return ExternalDBApplicationResult.Succeeded;
        }
    }
}
