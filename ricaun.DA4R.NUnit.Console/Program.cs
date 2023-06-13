using ricaun.RevitTest.Command;

namespace ricaun.DA4R.NUnit.Console
{
    // ricaun.DA4R.NUnit.Console.exe --file "C:\Users\ricau\Downloads\SampleTest\RevitAddin.RevitApplication.Tests\2024\RevitAddin.RevitApplication.Tests.dll" -o "console" -l

    public class Program
    {
        public static void Main(string[] args)
        {
            RunTest.ParseArguments<DA4RTestService>(args);

            //await RunZipTest();
        }

        //private static async Task RunZipTest()
        //{
        //    var appName = "ricaun_DA4R_NUnit_Test";
        //    var appBundle = $".\\Bundle\\ricaun.DA4R.NUnit.bundle.zip";
        //    var inputZip = $".\\Resources\\SampleTest.zip";
        //    inputZip = @"C:\Users\ricau\Downloads\SampleTest\RevitAddin.RevitApplication.Tests.zip";

        //    IDesignAutomationService designAutomationService = new RevitDesignAutomationService(appName)
        //    {
        //        EngineVersions = new[] {
        //            //"2020",
        //            "2021",
        //            //"2022",
        //            //"2023",
        //            //"2024",
        //        },
        //        EnableConsoleLogger = true,
        //        //EnableParameterConsoleLogger = true,
        //        //EnableReportConsoleLogger = true,
        //    };

        //    await designAutomationService.Initialize(appBundle);

        //    var option = new ParameterOptions()
        //    {
        //        Input = inputZip
        //    };

        //    var result = await designAutomationService.Run<ParameterOptions>(option);

        //    var output = option.Output;

        //    PrintOutput(output);

        //    await designAutomationService.Delete();
        //}

        //private static void PrintOutput(OutputModel output)
        //{
        //    if (output is null) return;
        //    System.Console.WriteLine("-------------------------------------");
        //    System.Console.WriteLine($"VersionName: {output.VersionName}");
        //    System.Console.WriteLine($"VersionBuild: {output.VersionBuild}");
        //    System.Console.WriteLine($"TimeStart: {output.TimeStart}");
        //    System.Console.WriteLine($"TimeFinish: {output.TimeFinish}");
        //    System.Console.WriteLine($"Success: {output.Success}");
        //    System.Console.WriteLine($"Tests: {output.Tests.Count}");
        //    var tests = output.Tests.SelectMany(e => e.Tests.SelectMany(e => e.Tests));
        //    foreach (var test in tests)
        //    {
        //        System.Console.WriteLine($"\t{test}");
        //    }
        //    System.Console.WriteLine("-------------------------------------");
        //}
    }
}