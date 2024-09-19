using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

namespace ricaun.DA4R.NUnit.Revit.DA
{
    public interface IDesignAutomation
    {
        bool Execute(Application application, string filePath, Document document);
    }
}