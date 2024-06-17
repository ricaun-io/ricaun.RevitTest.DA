using Autodesk.Revit.DB;

namespace ricaun.DA4R.NUnit.Tests
{
    public class ProjectInfoTests
    {
        interface IProjectInfo : IElement<ProjectInfo> { }
        interface IElement<T> where T : Element { }
    }

    public class ForgeTypeIdTests
    {
        interface IForgeTypeId : IElement<ForgeTypeId> { }
        interface IElement<T> where T : ForgeTypeId { }
    }
}