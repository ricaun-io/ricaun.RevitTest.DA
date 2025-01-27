using ricaun.RevitTest.Command;
using System;

namespace ricaun.DA4R.NUnit.Console
{
    public class App
    {
        public static string Name { get; set; } = "ricaun_DA4R_NUnit_Test";
        public static string Bundle { get; set; } = $".\\Resources\\ricaun.DA4R.NUnit.bundle.zip";
        public static string ForgeEnvironment { get; set; } = "release";
        public static string ApsAccessToken => 
            Environment.GetEnvironmentVariable("APS_ACCESS_TOKEN") ?? 
            Environment.GetEnvironmentVariable("FORGE_ACCESS_TOKEN");
    }
}