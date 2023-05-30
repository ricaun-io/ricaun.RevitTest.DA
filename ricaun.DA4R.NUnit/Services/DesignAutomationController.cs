﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.DA4R.NUnit.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace ricaun.DA4R.NUnit.Services
{
    public class DesignAutomationController
    {
        public static bool Execute(Application revitApp)
        {
            return Execute(revitApp, null, null);
        }

        public static bool Execute(Application application, string filePath, Document revitDoc)
        {
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

            //OutputModelUtils.ZipFolder();

            return true;
        }

    }
}
