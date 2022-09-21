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
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(this.GetType().Assembly.FullName);
            Console.WriteLine("--------------------------------------------------");

            RevitParameters.AddParameter(application, application.GetApplication());
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

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"RevitApp: {data.RevitApp} FilePath: {data.FilePath} RevitDoc: {data.RevitDoc}");
            Console.WriteLine($"RevitNET: {data.RevitApp.GetType().Assembly.FullName}");
            Console.WriteLine("--------------------------------------------------");

            RevitParameters.AddParameter(data.RevitApp);

            //var revitNet = data.RevitApp;

            //Console.WriteLine("--------------------------------------------------");
            //foreach (var item in revitNet.GetType().GetNestedTypes())
            //{
            //    Console.WriteLine($"Type: {item}");
            //}
            //foreach (var member in revitNet.GetType().GetMembers())
            //{
            //    Console.WriteLine($"{member.MemberType} {member}");
            //}
            //Console.WriteLine("--------------------------------------------------");
            //Console.WriteLine("--------------------------------------------------");

            e.Succeeded = DesignAutomationController.Execute(data.RevitApp, data.FilePath, data.RevitDoc);
        }
    }
}
