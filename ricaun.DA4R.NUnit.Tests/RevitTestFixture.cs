using Autodesk.Revit.ApplicationServices;
using NUnit.Framework;
using System;

namespace ricaun.DA4R.NUnit.Tests
{
    [TestFixture(1)]
    public class RevitTestFixture
    {
        readonly int value;
        private Application application;
        public RevitTestFixture(int i)
        {
            this.value = i;
        }

        [OneTimeSetUp]
        public void Setup(Application application)
        {
            this.application = application;
        }

        [Test]
        public void ValueTest()
        {
            Console.WriteLine(value);
            Assert.NotZero(value);
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