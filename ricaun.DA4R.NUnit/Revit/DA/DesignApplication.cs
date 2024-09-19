﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using ricaun.DA4R.NUnit.Revit.ExternalServer;
using System;

namespace ricaun.DA4R.NUnit.Revit.DA
{
    public abstract class DesignApplication : IExternalDBApplication, IDesignAutomation
    {
        /// <summary>
        /// Use ExternalService to execute the IDesignAutomation.Execute, this make the Execute run in the AddIn context.
        /// </summary>
        public virtual bool UseExternalService => true;
        public ControlledApplication Application { get; private set; }
        public abstract void OnStartup();
        public abstract void OnShutdown();
        public abstract bool Execute(Application application, string filePath, Document document);

        private IExternalDBApplication designApplication;
        private DesignAutomationSingleExternalServer externalServer;
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            Application = application;

            designApplication = DesignApplicationLoader.LoadVersion(this);

            if (designApplication is IExternalDBApplication)
            {
                return designApplication.OnStartup(application);
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"FullName: \t{GetType().Assembly.FullName}");
            Console.WriteLine($"AddInName: \t{Application.ActiveAddInId?.GetAddInName()}");
            Console.WriteLine("----------------------------------------");

            try
            {
                externalServer = new DesignAutomationSingleExternalServer(this).Register();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DesignAutomationSingleExternalServer: \t{ex.Message}");
            }

            OnStartup();
            DesignAutomationBridge.DesignAutomationReadyEvent += DesignAutomationReadyEvent;

            return ExternalDBApplicationResult.Succeeded;
        }

        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            Application = application;

            if (designApplication is IExternalDBApplication)
            {
                try
                {
                    return designApplication.OnShutdown(application);
                }
                finally
                {
                    DesignApplicationLoader.Dispose();
                }
            }

            externalServer?.RemoveServer();

            OnShutdown();
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationReadyEvent;

            return ExternalDBApplicationResult.Succeeded;
        }


        private void DesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
        {
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationReadyEvent;

            var data = e.DesignAutomationData;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"RevitApp: {data.RevitApp} \tFilePath: {data.FilePath} \tRevitDoc: {data.RevitDoc} \tAddInName:{data.RevitApp.ActiveAddInId?.GetAddInName()}");
            Console.WriteLine("--------------------------------------------------");

            if (externalServer is not null && UseExternalService)
            {
                e.Succeeded = externalServer.ExecuteService(data.RevitApp, data.FilePath, data.RevitDoc);
                return;
            }

            e.Succeeded = Execute(data.RevitApp, data.FilePath, data.RevitDoc);
        }
    }
}