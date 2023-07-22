using ricaun.RevitTest.Command;

namespace ricaun.DA4R.NUnit.Console
{
    // ricaun.DA4R.NUnit.Console.exe --file "C:\Users\ricau\Downloads\SampleTest\RevitAddin.RevitApplication.Tests\2024\RevitAddin.RevitApplication.Tests.dll" -o "console" -l

    public class Program
    {
        public static void Main(string[] args)
        {
            LogInfo();
            RunTest.ParseArguments<DA4RTestService>(args);
            LogInfo();
        }

        public static void LogInfo()
        {
            var name = typeof(Program).Assembly.GetName();
            Log.WriteLine($"------------------------------- {name.Name} {name.Version!.ToString(3)} -------------------------------");
        }
    }
}