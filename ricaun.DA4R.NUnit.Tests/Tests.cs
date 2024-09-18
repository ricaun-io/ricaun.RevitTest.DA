using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using NUnit.Framework;
using System;

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