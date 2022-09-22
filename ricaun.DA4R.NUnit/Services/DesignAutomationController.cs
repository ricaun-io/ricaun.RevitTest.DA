using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.DA4R.NUnit.Extensions;
using ricaun.DA4R.NUnit.Models;
using ricaun.NUnit;
using System;
using System.Diagnostics;
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

            output.Save();

            UnZipAndTestFiles(application, output);

            output.Success = !output.Tests.Any(t => t.Success == false);
            output.TimeFinish = DateTime.UtcNow;

            var text = output.Save();
            Console.WriteLine(text);
#if DEBUG
            System.Windows.Clipboard.SetText(text);
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

                    // Test Main Directory
                    TestDirectory(output, zipDestination);

                    // Try to find Directory
                    if (output.Tests.Count == 0)
                    {
                        if (int.TryParse(application.VersionNumber, out int version))
                        {
                            for (int i = 1; i < 10; i++)
                            {
                                var versionTest = version - i;
                                Console.WriteLine($"Test Folder: {versionTest}");
                                var versionTestDirectory = Path.Combine(zipDestination, versionTest.ToString());
                                if (Directory.Exists(versionTestDirectory))
                                {
                                    TestDirectory(output, versionTestDirectory);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void TestDirectory(OutputModel output, string directory)
        {
            foreach (var filePath in Directory.GetFiles(directory, "*.dll"))
            {
                var fileName = Path.GetFileName(filePath);
                try
                {
                    if (TestEngine.ContainNUnit(filePath))
                    {
                        Console.WriteLine($"Test File: {fileName}");
                        Console.WriteLine("--------------------------------------------------");
                        foreach (var parameter in RevitParameters.Parameters)
                        {
                            Console.WriteLine($"RevitParameters: {parameter}");
                        }
                        Console.WriteLine("--------------------------------------------------");

                        var modelTest = TestEngine.TestAssembly(
                            filePath,
                            RevitParameters.Parameters);

                        Console.WriteLine($"{modelTest}");
                        Console.WriteLine("--------------------------------------------------");

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
