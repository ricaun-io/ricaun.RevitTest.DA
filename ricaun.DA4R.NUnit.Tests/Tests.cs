﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using NUnit.Framework;
using System;

[assembly: System.Reflection.AssemblyMetadata("NUnit.Verbosity", "3")]
#if !DEBUG
#if NET
[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", "RICAUN_REVIT_TEST_APPLICATION_DA4R_ONLINE_TEST_2025")]
#else
[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", "RICAUN_REVIT_TEST_APPLICATION_DA4R_ONLINE_TEST")]
#endif
#endif

//[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", @"D:\Users\ricau\source\repos\ricaun.DA4R.NUnit\ricaun.DA4R.NUnit.Console\bin\Debug\ricaun.DA4R.NUnit.Console.exe")]
//[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", @"..\..\..\..\ricaun.DA4R.NUnit.Console\bin\Debug\ricaun.DA4R.NUnit.Console.exe")]

[assembly: System.Reflection.AssemblyMetadata("NUnit.Application", "D:\\Users\\ricau\\source\\repos\\ricaun.DA4R.NUnit\\ricaun.DA4R.NUnit\\bin\\ReleaseFiles\\ricaun.DA4R.NUnit.Console.zip")]


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
            Console.WriteLine(application.VersionBuild);
        }
    }
}