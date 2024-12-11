using ricaun.RevitTest.Command;
using System;

namespace ricaun.DA4R.NUnit.Console
{
    public class App
    {
        public static string Name { get; set; } = "ricaun_DA4R_NUnit_Test";
        public static string Bundle { get; set; } = $".\\Resources\\ricaun.DA4R.NUnit.bundle.zip";
        public static string ForgeEnvironment { get; set; } = "release";
        public static string ForgeClientId => Environment.GetEnvironmentVariable("APS_CLIENT_ID") ?? Environment.GetEnvironmentVariable("FORGE_CLIENT_ID");
        public static string ForgeClientSecret => Environment.GetEnvironmentVariable("APS_CLIENT_SECRET") ?? Environment.GetEnvironmentVariable("FORGE_CLIENT_SECRET");
        public static string ForgeClientCustomHeaderValue => Environment.GetEnvironmentVariable("APS_CLIENT_CUSTOM_HEADER_VALUE") ?? Environment.GetEnvironmentVariable("FORGE_CLIENT_CUSTOM_HEADER_VALUE");
    }
}