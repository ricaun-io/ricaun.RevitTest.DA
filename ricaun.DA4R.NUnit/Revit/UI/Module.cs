using ricaun.DA4R.NUnit.Revit.UI.Extensions;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ricaun.DA4R.NUnit.Revit.UI
{
    class Module
    {
#pragma warning disable CA2255 // The 'ModuleInitializer' attribute should not be used in libraries
        [ModuleInitializer]
#pragma warning restore CA2255 // The 'ModuleInitializer' attribute should not be used in libraries
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

#if NETFRAMEWORK
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class ModuleInitializerAttribute : Attribute { }
}
#endif
