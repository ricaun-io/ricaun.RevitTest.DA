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
            output.TimeStart = DateTime.UtcNow;

            UnZipAndTestFiles(application, output);

            output.TimeFinish = DateTime.UtcNow;

            var text = output.Save();
            Console.WriteLine(text);


            return true;
        }

        private static void UnZipAndTestFiles(Application application, OutputModel output)
        {
            var versionNumber = application.VersionNumber;

            if (Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip").FirstOrDefault() is string zipFile)
            {
                if (ZipExtension.ExtractToFolder(zipFile, out string zipDestination))
                {
                    Console.WriteLine($"Test Unzip: {Path.GetFileName(zipFile)}");
                    foreach (var versionDirectory in Directory.GetDirectories(zipDestination))
                    {
                        if (Path.GetFileName(versionDirectory).Equals(versionNumber))
                        {
                            Console.WriteLine($"Test VersionNumber: {versionNumber}");
                            TestDirectory(output, versionDirectory);
                            return;
                        }
                    }
                    TestDirectory(output, zipDestination);
                }
            }
        }

        private static void TestDirectory(OutputModel output, string directory)
        {
            foreach (var filePath in Directory.GetFiles(directory, "*.dll"))
            {
                var fileName = Path.GetFileName(filePath);
                Console.WriteLine($"Test File: {fileName}");
                try
                {
                    if (TestEngine.ContainNUnit(filePath))
                    {
                        Console.WriteLine("--------------------------------------------------");
                        foreach (var parameter in RevitParameters.Parameters)
                        {
                            Console.WriteLine($"RevitParameters: {parameter}");
                        }
                        Console.WriteLine("--------------------------------------------------");

                        var modelTest = TestEngine.TestAssembly(
                            filePath,
                            RevitParameters.Parameters);

                        output.Tests.Add(modelTest);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    // output.Tests.Add(fileName);
                }
            }
        }
    }
}
