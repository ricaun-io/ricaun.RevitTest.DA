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
        [TestCase("ricaun.NUnit")]
        [TestCase("NUnit")]
        public void GetTargetFramework(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(e => e.GetName().Name == name))
            {
                var framework = assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
                Console.WriteLine($"{name} \t{framework}");
                Console.WriteLine($"{assembly.Location}");
            }
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