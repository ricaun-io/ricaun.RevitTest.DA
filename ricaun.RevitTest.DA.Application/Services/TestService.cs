﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.RevitTest.DA.Application.Extensions;
using ricaun.RevitTest.DA.Application.Models;
using ricaun.NUnit;
using System;
using System.IO;
using System.Linq;

namespace ricaun.RevitTest.DA.Application.Services
{
    public static class TestService
    {
        public static void UnZipAndTestFiles(Autodesk.Revit.ApplicationServices.Application application, OutputModel output)
        {
            var versionNumber = application.VersionNumber;

            if (Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip").FirstOrDefault() is string zipFile)
            {
                if (ZipExtension.ExtractToFolder(zipFile, out string zipDestination))
                {
                    Console.WriteLine($"Test Unzip: {Path.GetFileName(zipFile)} {Path.GetFileName(zipDestination)}");
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
                        Console.WriteLine($"Test File: {fileName} [{TestEngine.Version} - {TestEngine.VersionNUnit}]");
                        Console.WriteLine("--------------------------------------------------");
                        foreach (var parameter in RevitParameters.Parameters)
                        {
                            Console.WriteLine($"RevitParameters: {parameter}");
                        }
                        Console.WriteLine("--------------------------------------------------");
                        TestEngineFilter.Reset();
                        foreach (var testName in TestEngine.GetTestFullNames(filePath))
                        {
                            Console.WriteLine($"\t{testName}");
                        }

                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine($"Test Start: {fileName}");

                        var modelTest = TestEngine.TestAssembly(
                            filePath,
                            RevitParameters.Parameters);

                        Console.WriteLine($"Test Finish: {modelTest}");
                        Console.WriteLine("--------------------------------------------------");

                        var tests = modelTest.Tests.SelectMany(e => e.Tests);
                        if (tests.Any() == false)
                        {
                            Console.WriteLine($"Error: {modelTest.Message}");
                            try
                            {
                                var ex = new Exception(modelTest.Message.Split('\n').FirstOrDefault());
                                modelTest = TestEngine.Fail(filePath, ex);
                            }
                            catch { }
                        }

                        foreach (var ex in TestEngine.Exceptions)
                        {
                            Console.WriteLine($"Exception: {ex}");
                        }

                        output.Tests.Add(modelTest);
                    }

                }
                catch (Exception ex)
                {
                    var testFail = new Exception($"Test File: {fileName}", ex);
                    var testFailModel = TestEngine.Fail(filePath, testFail);
                    output.Tests.Add(testFailModel);
                    Console.WriteLine($"{fileName} {ex}");
                }
            }
        }
    }
}
