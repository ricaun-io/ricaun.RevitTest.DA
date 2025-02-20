using System.Reflection;

[assembly: AssemblyMetadata("NUnit.Timeout", "5")] // Set timeout 5 minutes
[assembly: AssemblyMetadata("NUnit.Verbosity", "3")]
#if !DEBUG
//[assembly: AssemblyMetadata("NUnit.Application", "RICAUN_REVIT_TEST_APPLICATION_DA4R_ONLINE_TEST")]
[assembly: AssemblyMetadata("NUnit.Application", "..\\..\\..\\..\\..\\ricaun.DA4R.NUnit\\bin\\ReleaseFiles\\ricaun.DA4R.NUnit.Console.zip")]
#endif

#if DEBUG
[assembly: AssemblyMetadata("NUnit.Application", "..\\..\\..\\..\\ricaun.DA4R.NUnit\\bin\\ReleaseFiles\\ricaun.DA4R.NUnit.Console.zip")]
#endif