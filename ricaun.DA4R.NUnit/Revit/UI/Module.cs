using ricaun.DA4R.NUnit.Revit.UI.Extensions;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ricaun.DA4R.NUnit.Revit.UI
{
    class Module
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            //Debug.WriteLine($"Module: {typeof(Module).Assembly}");
            using (AppDomainUtils.AssemblyResolveDisposable())
            {
                CosturaUtility.Initialize();
                TestUtils.Initialize();
            }
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class ModuleInitializerAttribute : Attribute { }
}
