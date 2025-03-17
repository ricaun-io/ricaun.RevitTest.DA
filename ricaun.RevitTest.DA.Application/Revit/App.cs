using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.Revit.DA;
using ricaun.RevitTest.DA.Application.Services;

namespace ricaun.RevitTest.DA.Application.Revit
{
    public class App : DesignApplication
    {
        public override bool Execute(Autodesk.Revit.ApplicationServices.Application application, string filePath, Document document)
        {
            return new DesignAutomationController().Execute(application, filePath, document);
        }
    }
}
