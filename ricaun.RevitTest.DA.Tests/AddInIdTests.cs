using Autodesk.Revit.ApplicationServices;
using NUnit.Framework;
using System;

namespace ricaun.RevitTest.DA.Tests
{
    public class AddInIdTests
    {
        private Application application;

        [OneTimeSetUp]
        public void Setup(Application application)
        {
            this.application = application;
        }

        [Test]
        public void ActiveAddInId_ShouldBe_Valid()
        {
            Console.WriteLine($"ActiveAddInId: \t{application.ActiveAddInId?.GetAddInName()} \t{application.ActiveAddInId?.GetGUID()}");
            Assert.IsNotNull(application.ActiveAddInId);
        }
    }
}