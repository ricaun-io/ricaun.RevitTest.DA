using Autodesk.Revit.DB;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace ricaun.DA4R.NUnit.Tests
{
    public class AssemblyTests
    {
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