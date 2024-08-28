using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using ricaun.DA4R.NUnit.Services;
using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace ricaun.DA4R.NUnit.Revit
{
    public class App : IExternalDBApplication
    {
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(this.GetType().Assembly.FullName);
            Console.WriteLine(typeof(ricaun.NUnit.TestEngine).Assembly.FullName);
            Console.WriteLine($"Framework: {typeof(App).Assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName}");
            Console.WriteLine("--------------------------------------------------");

            DesignAutomationBridge.DesignAutomationReadyEvent += DesignAutomationBridge_DesignAutomationReadyEvent;
            return ExternalDBApplicationResult.Succeeded;
        }

        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationBridge_DesignAutomationReadyEvent;
            Console.WriteLine("--------------------------------------------------");
            return ExternalDBApplicationResult.Succeeded;
        }

        private void DesignAutomationBridge_DesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
        {
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationBridge_DesignAutomationReadyEvent;

            var data = e.DesignAutomationData;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"RevitApp: {data.RevitApp} FilePath: {data.FilePath} RevitDoc: {data.RevitDoc}");
            Console.WriteLine($"RevitNET: {data.RevitApp.GetType().Assembly.FullName}");
            Console.WriteLine("--------------------------------------------------");

            e.Succeeded = DesignAutomationController.Execute(data.RevitApp, data.FilePath, data.RevitDoc);
        }
    }
}
