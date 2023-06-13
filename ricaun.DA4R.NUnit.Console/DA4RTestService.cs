using Autodesk.Forge.Oss.DesignAutomation;
using Autodesk.Forge.Oss.DesignAutomation.Services;
using ricaun.DA4R.NUnit.Console.Utils;
using ricaun.NUnit;
using ricaun.Revit.Installation;
using ricaun.RevitTest.Command;
using ricaun.RevitTest.Command.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ricaun.DA4R.NUnit.Console
{
    public class App
    {
        public static string Name { get; set; } = "ricaun_DA4R_NUnit_Test";
        public static string Bundle { get; set; } = $".\\Resources\\ricaun.DA4R.NUnit.bundle.zip";
    }

    public class DA4RTestService : IRunTestService
    {
        private const int MINIMAL_ENGINE_VERSION = 2019;

        public string[] GetTests(string filePath)
        {
            return TestEngine.GetTestFullNames(filePath);
        }

        public bool RunTests(
            string fileToTest,
            int revitVersionNumber,
            Action<string> actionOutput = null,
            bool forceToOpenNewRevit = false,
            bool forceToWaitRevit = false,
            bool forceToCloseRevit = false,
            params string[] testFilters)
        {
            if (revitVersionNumber == 0)
            {
                if (RevitUtils.TryGetRevitVersion(fileToTest, out revitVersionNumber))
                {
                    Log.WriteLine($"TryGetRevitVersion: {revitVersionNumber}");
                }
            }

            if (revitVersionNumber < MINIMAL_ENGINE_VERSION)
            {
                Log.WriteLine($"Revit version not supported: {revitVersionNumber}");
                return false;
            }

            return Task.Run(async () =>
            {
                try
                {
                    await Run(fileToTest, revitVersionNumber, actionOutput);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.WriteLine($"Exception: {ex}");
                    return false;
                }
            }).GetAwaiter().GetResult();
        }

        private async Task Run(string filePath, int revitVersionNumber, Action<string> actionOutput)
        {
            Log.WriteLine($"Version: {revitVersionNumber}");

            if (!Path.GetExtension(filePath).EndsWith("dll"))
            {
                return;
            }

            var inputZip = ZipExtension.CreateFromFileToTemporaryDirectory(filePath);

            using var zipFileTemporary = new ZipFileTemporary(filePath);

            Log.WriteLine($"Create Input: {zipFileTemporary.ZipFilePath}");
            Log.WriteLine("-------------------------------------");

            IDesignAutomationService designAutomationService = new RevitDesignAutomationService(App.Name)
            {
                EngineVersions = new[] { revitVersionNumber.ToString() },
                EnableConsoleLogger = Log.Enabled,
                //EnableParameterConsoleLogger = true,
                //EnableReportConsoleLogger = true,
            };

            await designAutomationService.Initialize(App.Bundle);

            var option = new ParameterOptions()
            {
                Input = zipFileTemporary.ZipFilePath,
            };

            var result = await designAutomationService.Run<ParameterOptions>(option);

            var output = option.Output;

            PrintOutput(output);

            try
            {
                Log.WriteLine($"OutputZip: {option.OutputZip}");

                var zipCopyPathBack = await DownloadUtils.GetFileAsync(option.OutputZip, Path.GetTempFileName() + ".zip");

                Log.WriteLine($"ExtractToDirectoryIfNewer: {Path.GetDirectoryName(filePath)}");

                ZipExtension.ExtractToDirectoryIfNewer(zipCopyPathBack, Path.GetDirectoryName(filePath));

                if (File.Exists(zipCopyPathBack))
                    File.Delete(zipCopyPathBack);
            }
            catch (Exception ex) { Log.WriteLine(ex); }

            Log.WriteLine("-------------------------------------");

            actionOutput?.Invoke(null); // Force to clear file
            actionOutput?.Invoke(output.Tests.FirstOrDefault().ToJson());

            await designAutomationService.Delete();
        }

        private static void PrintOutput(OutputModel output)
        {
            if (output is null) return;
            Log.WriteLine("-------------------------------------");
            Log.WriteLine($"VersionName: {output.VersionName}");
            Log.WriteLine($"VersionBuild: {output.VersionBuild}");
            Log.WriteLine($"TimeStart: {output.TimeStart}");
            Log.WriteLine($"TimeFinish: {output.TimeFinish}");
            Log.WriteLine($"Success: {output.Success}");
            Log.WriteLine($"Tests: {output.Tests.Count}");
            var tests = output.Tests.SelectMany(e => e.Tests.SelectMany(e => e.Tests));
            foreach (var test in tests)
            {
                Log.WriteLine($"\t{test}");
            }
            Log.WriteLine("-------------------------------------");
        }
    }
}