using Autodesk.Revit.DB.ExternalService;
using ricaun.DA4R.NUnit.Revit.ExternalServer;

namespace ricaun.DA4R.NUnit.Revit
{
    public interface IDesignAutomationExternalServer : IExternalServer
    {
        public bool Execute(DesignAutomationExternalData data);
    }
}