using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.RevitTest.DA.Application.Revit.UI.Services;
using ricaun.RevitTest.DA.Application.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ricaun.RevitTest.DA.Application.Revit.UI.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            using (new CurrentDirectory())
            {
                var path = @"Resources\SampleTest.zip";
                CopyFile(path);
                new DesignAutomationController().Execute(uiapp.Application);
            }

            return Result.Succeeded;
        }

        private void CopyFile(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (File.Exists(fileName))
            {
                Console.WriteLine($"Delete {fileName}");
                File.Delete(fileName);
            }
            File.Copy(filePath, fileName);
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
