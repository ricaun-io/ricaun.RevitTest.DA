using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;

namespace ricaun.DA4R.NUnit.Revit.ExternalServer
{
    public class DesignAutomationExternalData : IExternalData
    {
        public Application Application { get; set; }
        public string FilePath { get; set; }
        public Document Document { get; set; }
    }
}