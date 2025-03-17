using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.RevitTest.DA.Application.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;

namespace ricaun.RevitTest.DA.Application.Services
{
    public class DesignAutomationController
    {
        public bool Execute(Autodesk.Revit.ApplicationServices.Application application, string filePath = null, Document revitDoc = null)
        {
            RevitParameters.AddParameter(application, application.GetControlledApplication());

            Console.WriteLine($"Execute: {typeof(DesignAutomationController).Assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName}");
            Console.WriteLine($"TestEngine: {typeof(ricaun.NUnit.TestEngine).Assembly.FullName}");

            var output = new OutputModel();

            output.VersionName = application.VersionName;
            output.VersionBuild = application.VersionBuild;
            output.TimeStart = DateTime.UtcNow;

            output.Save();

            TestService.UnZipAndTestFiles(application, output);

            output.Success = !output.Tests.Any(t => t.Success == false);
            output.TimeFinish = DateTime.UtcNow;

            var text = output.Save();
            Console.WriteLine(text);

#if DEBUG
            System.Windows.Clipboard.SetText(text);
#endif

            OutputModelUtils.InputZip();

            return true;
        }

    }
}
