using Nuke.Common;
using ricaun.Nuke.Components;

public interface ITestConsole : IHazTest, IBuildConsole
{
    Target TestConsole => _ => _
        .TriggeredBy(BuildConsole)
        .Before(Release)
        .OnlyWhenStatic(() => IsLocalBuild || IsServerBuild)
        .Executes(() =>
        {
            var TestLocalProjectName = "*.Tests";
            TestProjects(TestLocalProjectName);
        });
}
