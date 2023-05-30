using System;
using System.Diagnostics;

namespace ricaun.DA4R.NUnit.Revit.UI
{
    public static class TestUtils
    {
        public static string Initialize()
        {
            bool initialize = ricaun.NUnit.TestEngine.Initialize(out string testInitialize);
            //Debug.WriteLine("");
            //Debug.WriteLine($"TestEngine: {initialize} {testInitialize}");
            //Debug.WriteLine("");
            return testInitialize;
        }

        public static string GetInitialize()
        {
            ricaun.NUnit.TestEngine.Initialize(out string testInitialize);
            return testInitialize;
        }
    }
}