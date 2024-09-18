using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using NUnit.Framework;
using System;

[assembly: System.Reflection.AssemblyMetadata("NUnit.Timeout", "5")] // Set timeout 5 minutes
[assembly: System.Reflection.AssemblyMetadata("NUnit.Verbosity", "3")]
#if !DEBUG
[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", "RICAUN_REVIT_TEST_APPLICATION_DA4R_ONLINE_TEST")]
#endif

#if DEBUG
[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", "..\\..\\..\\..\\ricaun.DA4R.NUnit\\bin\\ReleaseFiles\\ricaun.DA4R.NUnit.Console.zip")]
#endif

namespace ricaun.DA4R.NUnit.Tests
{
    public class Tests
    {
        private Application application;

        [OneTimeSetUp]
        public void Setup(Application application)
        {
            this.application = application;
        }

        [Test]
        public void RevitTests()
        {
            Assert.IsNotNull(application);
            Console.WriteLine($"VersionBuild: {application.VersionBuild}");
            Console.WriteLine($"ActiveAddInId: \t{application.ActiveAddInId?.GetAddInName()} \t{application.ActiveAddInId?.GetGUID()}");
        }
    }

    public class ControlledTests
    {
        private ControlledApplication application;

        [OneTimeSetUp]
        public void Setup(ControlledApplication application)
        {
            this.application = application;
        }

        [Test]
        public void RevitTests()
        {
            Assert.IsNotNull(application);
            Console.WriteLine($"VersionBuild: {application.VersionBuild}");
            Console.WriteLine($"ActiveAddInId: \t{application.ActiveAddInId?.GetAddInName()} \t{application.ActiveAddInId?.GetGUID()}");
        }
    }
}