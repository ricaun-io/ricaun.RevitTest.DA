using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.DA4R.NUnit.Revit.DA;
using ricaun.DA4R.NUnit.Services;

namespace ricaun.DA4R.NUnit.Revit
{
    public class App : DesignApplication
    {
        public override void OnStartup()
        {
            
        }
        public override void OnShutdown()
        {
            
        }
        public override bool Execute(Application application, string filePath, Document document)
        {
            return new DesignAutomationController().Execute(application, filePath, document);
        }
    }
}
