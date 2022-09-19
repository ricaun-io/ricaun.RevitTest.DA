using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using ricaun.DA4R.NUnit.Services;
using System;

namespace ricaun.DA4R.NUnit.Revit
{
    public class App : IExternalDBApplication
    {
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            DesignAutomationBridge.DesignAutomationReadyEvent += DesignAutomationBridge_DesignAutomationReadyEvent;
            return ExternalDBApplicationResult.Succeeded;
        }

        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationBridge_DesignAutomationReadyEvent;
            return ExternalDBApplicationResult.Succeeded;
        }

        private void DesignAutomationBridge_DesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
        {
            var data = e.DesignAutomationData;
            Console.WriteLine($"RevitApp: {data.RevitApp} FilePath: {data.FilePath} RevitDoc: {data.RevitDoc}");
            e.Succeeded = DesignAutomationController.Execute(data.RevitApp, data.FilePath, data.RevitDoc);
        }
    }
}
