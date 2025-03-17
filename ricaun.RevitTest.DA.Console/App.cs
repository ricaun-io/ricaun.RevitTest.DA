using ricaun.RevitTest.Command;
using System;

namespace ricaun.RevitTest.DA.Console
{
    public class App
    {
        public static string Name { get; set; } = "RevitTest_DA";
        public static string Bundle { get; set; } = $".\\Resources\\ricaun.RevitTest.DA.Application.bundle.zip";
        public static string ForgeEnvironment { get; set; } = "release";
        public static string ApsAccessToken => 
            Environment.GetEnvironmentVariable("APS_ACCESS_TOKEN") ?? 
            Environment.GetEnvironmentVariable("FORGE_ACCESS_TOKEN");
    }
}