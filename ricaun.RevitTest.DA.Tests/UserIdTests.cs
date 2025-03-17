using Autodesk.Revit.ApplicationServices;
using NUnit.Framework;
using System;

namespace ricaun.RevitTest.DA.Tests
{
    public class UserIdTests
    {
        private Application application;

        [OneTimeSetUp]
        public void Setup(Application application)
        {
            this.application = application;
        }

        [Test]
        public void UserId_ShouldBe_Valid()
        {
            // This test only works if the 'adsk3LeggedToken' is provided in the DA4R configuration file.
            Console.WriteLine(application.Username);
            if (string.IsNullOrEmpty(application.LoginUserId))
            {
                Assert.Ignore($"The user '{application.Username}' does not have a LoginUserId.");
            }    
        }
    }
}