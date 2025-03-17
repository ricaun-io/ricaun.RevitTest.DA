using ricaun.RevitTest.Command;

namespace ricaun.RevitTest.DA.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunTest.ParseArguments<DA4RTestService>(args);
        }
    }
}