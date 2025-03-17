using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;
using System.Linq;

namespace ricaun.RevitTest.DA.Application.Revit.UI
{
    [AppLoader]
    public class AppUI : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("DA4R");
            ribbonPanel.CreatePushButton<Commands.Command>("NUnit")
               .SetLargeImage("/UIFrameworkRes;component/ribbon/images/add.ico");
            //.SetLargeImage(Properties.Resources.Revit.GetBitmapSource());

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"TestEngine: {ricaun.NUnit.TestEngine.Initialize(out string testInitialize)} {testInitialize}");
            Console.WriteLine("--------------------------------------------------");

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies().Where(e => e.GetName().Name == "nunit.framework"))
            {
                Console.WriteLine($"{item} \t{item.Location}");
            }
            Console.WriteLine("--------------------------------------------------");

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

}