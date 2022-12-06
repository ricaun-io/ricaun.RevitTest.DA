using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;
using System.Linq;

namespace ricaun.DA4R.NUnit.Revit.UI
{
    [AppLoader]
    public class AppUI : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("DA4R");
            ribbonPanel.CreatePushButton<Commands.Command>()
                .SetLargeImage(Properties.Resources.Revit.GetBitmapSource());

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(this.GetType().Assembly.FullName);
            Console.WriteLine(typeof(ricaun.NUnit.TestEngine).Assembly.FullName);
            Console.WriteLine($"ricaun.NUnit {ricaun.NUnit.TestEngine.Version} {ricaun.NUnit.TestEngine.VersionNUnit}");
            Console.WriteLine("--------------------------------------------------");

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies().Where(e => e.GetName().Name == "nunit.framework"))
            {
                Console.WriteLine($"{item} \t{item.Location}");
            }
            Console.WriteLine("--------------------------------------------------");
            //foreach (var item in AppDomain.CurrentDomain.GetAssemblies().Where(e => e.GetName().Name.Contains("Dynamo")))
            //{
            //    Console.WriteLine($"{item} {item.Location}");
            //}
            //Console.WriteLine("--------------------------------------------------");
            //Console.WriteLine(typeof(NUnitAttribute).Assembly);


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

}