using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.DA4R.NUnit.Revit.UI.Services;
using ricaun.DA4R.NUnit.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ricaun.DA4R.NUnit.Revit.UI.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            using (new CurrentDirectory())
            {
                CopyFile(@"C:\Users\ricau\source\repos\RevitAddin.UnitTest\RevitAddin.UnitTest\bin\ReleaseFiles\RevitAddin.UnitTest 1.0.0.zip");
                DesignAutomationController.Execute(uiapp.Application);
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
