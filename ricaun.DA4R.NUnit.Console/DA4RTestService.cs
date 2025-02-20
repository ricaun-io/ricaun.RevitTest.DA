using Autodesk.Forge.Oss.DesignAutomation;
using Autodesk.Forge.Oss.DesignAutomation.Services;
using ricaun.DA4R.NUnit.Console.Utils;
using ricaun.NUnit;
using ricaun.NUnit.Models;
using ricaun.Revit.Installation;
using ricaun.RevitTest.Command;
using ricaun.RevitTest.Command.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ricaun.DA4R.NUnit.Console
{
    public class DA4RTestService : IRunTestService
    {
        private const int MINIMAL_ENGINE_VERSION = 2019;
        private const int TIMEOUT_MINUTES = 10;

        private void LogApplicationInfo()
        {
            var assembly = this.GetType().Assembly;
            var name = assembly.GetName();
            var version = name.Version!.ToString(3);

            try
            {
                var fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                version = fileVersionInfo.ProductVersion.Split('+')[0];
            }
            catch { }

            Log.WriteLine($"{name.Name} {version}");
        }

        public string[] GetTests(string filePath)
        {
            LogApplicationInfo();

            var baseTests = TestEngine.GetTestFullNames(filePath);

            foreach (var ex in TestEngine.Exceptions)
                Log.WriteLine($"ERROR: {ex.Message}");

            if (baseTests.Length == 0)
            {
                Log.WriteLine($"ERROR: TestEngine.GetTestFullNames is empty, some class is breaking.");
            }
            return baseTests;
        }

        static int ConvertToRevitVersion(string revitVersion)
        {
            if (string.IsNullOrWhiteSpace(revitVersion))
            {
                return 0;
            }
            if (int.TryParse(revitVersion, out int revitVersionNumber))
            {
                return revitVersionNumber;
            }
            return 0;
        }

        public bool RunTests(string fileToTest,
            string revitVersion,
            Action<string> actionOutput = null,
            string forceLanguageToRevit = null,
            bool forceToOpenNewRevit = false,
            bool forceToCloseRevit = false,
            double timeoutMinutes = 0,
            params string[] testFilters)
        {
            LogApplicationInfo();

            var revitVersionNumber = ConvertToRevitVersion(revitVersion);

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

                return Task.Run(async () =>
                {
                    var ex = new Exception($"Revit version not supported: {revitVersionNumber}");
                    var testAssemblyModel = CreateTestAssemblyModelWithException(fileToTest, ex);

                    actionOutput?.Invoke(null); // Force to clear file
                    actionOutput?.Invoke(testAssemblyModel.ToJson());

                    await Task.Delay(500);

                    return false;
                }).GetAwaiter().GetResult();
            }

            return Task.Run(async () =>
            {
                try
                {
                    await Run(fileToTest, revitVersionNumber, forceLanguageToRevit, timeoutMinutes, actionOutput);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.WriteLine($"Exception: {ex}");

                    var testAssemblyModel = CreateTestAssemblyModelWithException(fileToTest, ex);

                    actionOutput?.Invoke(null); // Force to clear file
                    actionOutput?.Invoke(testAssemblyModel.ToJson());

                    await Task.Delay(500);

                    return false;
                }
            }).GetAwaiter().GetResult();
        }

        private TestAssemblyModel CreateTestAssemblyModelWithException(string fileToTest, Exception exception)
        {
            var testNames = GetTests(fileToTest);
            return TestExceptionUtils.CreateTestAssemblyModelWithException(fileToTest, testNames, exception);
        }

        private async Task Run(string filePath, int revitVersionNumber, string revitLanguage, double timeoutMinutes, Action<string> actionOutput)
        {

            revitLanguage = LanguageUtils.GetArgument(revitLanguage);

            Log.WriteLine($"Version: {revitVersionNumber}");
            Log.WriteLine($"Language: {revitLanguage}");

            if (timeoutMinutes <= 0) timeoutMinutes = TIMEOUT_MINUTES;
            if (timeoutMinutes != TIMEOUT_MINUTES) Log.WriteLine($"Timeout: {timeoutMinutes} minutes");

            if (!Path.GetExtension(filePath).EndsWith("dll"))
            {
                return;
            }

            var inputZip = ZipExtension.CreateFromFileToTemporaryDirectory(filePath);

            using var zipFileTemporary = new ZipFileTemporary(filePath);

            Log.WriteLine($"Create Input: {zipFileTemporary.ZipFilePath}");
            Log.WriteLine("-------------------------------------");
            Log.WriteLine($"Oss.DesignAutomation: {typeof(IDesignAutomationService).Assembly.GetName().Version.ToString(3)}");
            Log.WriteLine("-------------------------------------");

            var option = new ParameterOptions()
            {
                Language = revitLanguage,
                Input = zipFileTemporary.ZipFilePath,
                AccessToken = App.ApsAccessToken,
            };

            // Validate Autodesk AccessToken by getting UserInfo
            if (!string.IsNullOrEmpty(option.AccessToken))
            {
                Log.WriteLine($"AccessToken: ***");
                try
                {
                    var userInfo = await AspUserInfoUtils.GetUserInfo(option.AccessToken);
                    Log.WriteLine($"UserInfo: {userInfo}");
                }
                catch (Exception ex)
                {
                    Log.WriteLine($"UserInfo.Exception: {ex.Message}");
                    option.AccessToken = null;
                }
                Log.WriteLine("-------------------------------------");
            }

            var id = System.Diagnostics.Process.GetCurrentProcess().Id;
            var name = App.Name + "_" + id;
            IDesignAutomationService designAutomationService = new RevitDesignAutomationService(name)
            {
                EngineVersions = new[] { revitVersionNumber.ToString() },
                EnableConsoleLogger = Log.Enabled,
                //EnableParameterConsoleLogger = true,
                EnableReportConsoleLogger = Log.Enabled,
                RunTimeOutMinutes = timeoutMinutes,
                ForgeEnvironment = App.ForgeEnvironment,
            };

            await designAutomationService.Initialize(App.Bundle);

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
            actionOutput?.Invoke(output?.Tests?.FirstOrDefault().ToJson());

            await designAutomationService.Delete();

            if (result == false)
            {
                throw new Exception("Run Fail - Timeout? CheckLog?");
            }

            if (output.Tests.Count == 0)
            {
                throw new Exception("Run Fail - No tests found in the output.");
            }
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