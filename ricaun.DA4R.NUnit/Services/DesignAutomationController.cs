using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.DA4R.NUnit.Extensions;
using ricaun.DA4R.NUnit.Models;
using ricaun.NUnit;
using System;
using System.IO;
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

            UnZipAndTestFiles(application, output);

            var text = output.Save();
            Console.WriteLine(text);

#if DEBUG
            System.Windows.MessageBox.Show(text);
#endif

            return true;
        }

        private static void UnZipAndTestFiles(Application application, OutputModel output)
        {
            var versionNumber = application.VersionNumber;

            if (Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip").FirstOrDefault() is string zipFile)
            {
                if (ZipExtension.ExtractToFolder(zipFile, out string zipDestination))
                {
                    foreach (var versionDirectory in Directory.GetDirectories(zipDestination))
                    {
                        if (Path.GetFileName(versionDirectory).Equals(versionNumber))
                        {
                            Console.WriteLine($"Test VersionNumber: {versionNumber}");
                            TestDirectory(application, output, versionDirectory);
                        }
                    }

                    TestDirectory(application, output, zipDestination);
                }
            }
        }

        private static void TestDirectory(Application application, OutputModel output, string directory)
        {
            foreach (var filePath in Directory.GetFiles(directory, "*.dll"))
            {
                var fileName = Path.GetFileName(filePath);
                Console.WriteLine($"Test File: {fileName}");
                try
                {
                    if (TestEngine.ContainNUnit(filePath))
                    {
                        var modelTest = TestEngine.TestAssembly(
                            filePath,
                            application,
                            application.GetControlledApplication());

                        output.Tests.Add(modelTest);
                    }

                }
                catch (Exception)
                {
                    output.Tests.Add(fileName);
                }
            }
        }
    }
}
