using Autodesk.Revit.DB;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;

namespace ricaun.DA4R.NUnit.Tests
{
    public class AssemblyTests
    {
        [TestCase("ricaun.DA4R.NUnit")]
        public void GetTargetFramework(string name)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(e => e.GetName().Name == name);
            foreach (var assembly in assemblies)
            {
                var framework = assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
                Console.WriteLine($"{name} \t{framework}");
                Console.WriteLine($"{assembly.Location}");
            }
#if REVIT2019
            Assert.AreEqual(1, assemblies.Count());
#else
            Assert.AreEqual(2, assemblies.Count());
#endif
        }

        [Test]
        public void GetLocation()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(location);
        }

        [Test]
        public void GetAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(e => e.GetName().Name);
            foreach (var assembly in assemblies)
            {
                string location = null;
                try
                {
                    location = assembly.Location;
                }
                catch { }

                Console.WriteLine($"{assembly.GetName().Name} \t{assembly.GetName().Version} \t{location}");
            }
        }
    }
}