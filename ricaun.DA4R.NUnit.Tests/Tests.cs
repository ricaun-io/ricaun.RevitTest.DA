using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using NUnit.Framework;
using System;

[assembly: System.Reflection.AssemblyMetadata("NUnit.Verbosity", "2")]
[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", "D:\\Users\\ricau\\source\\repos\\ricaun.DA4R.NUnit\\ricaun.DA4R.NUnit\\bin\\ReleaseFiles\\ricaun.DA4R.NUnit.Console.zip")]

namespace ricaun.DA4R.NUnit.Tests
{
    public class Tests
    {
        [Test]
        public void RevitTests(Application application)
        {
            Assert.IsNotNull(application);
            Console.WriteLine(application.VersionBuild);
        }
    }
}