using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.Revit.DA;
using ricaun.DA4R.NUnit.Services;

namespace ricaun.DA4R.NUnit.Revit
{
    public class App : DesignApplication
    {
        public override bool Execute(Application application, string filePath, Document document)
        {
            return new DesignAutomationController().Execute(application, filePath, document);
        }
    }
}
