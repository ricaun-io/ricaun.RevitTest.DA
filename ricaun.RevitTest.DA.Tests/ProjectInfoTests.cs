using Autodesk.Revit.DB;

namespace ricaun.RevitTest.DA.Tests
{
    public class ProjectInfoTests
    {
        interface IProjectInfo : IElement<ProjectInfo> { }
        interface IElement<T> where T : Element { }
    }

#if REVIT2021_OR_GREATER
    public class ForgeTypeIdTests
    {
        interface IForgeTypeId : IElement<ForgeTypeId> { }
        interface IElement<T> where T : ForgeTypeId { }
    }
#endif
}