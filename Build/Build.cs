using Nuke.Common;
using Nuke.Common.Execution;
using ricaun.Nuke;
using ricaun.Nuke.Components;

class Build : NukeBuild, IPublishRevit, IBuildConsole, IGitPreRelease, ICustomAssetRelease, ITestConsole
{
    string IHazRevitPackageBuilder.ApplicationType => "DBApplication";
    string IHazRevitPackageBuilder.Application => "Revit.App";
    string IHazMainProject.MainName => "ricaun.RevitTest.DA.Application";
    bool IHazPackageBuilderProject.ReleasePackageBuilder => false;
    public static int Main() => Execute<Build>(x => x.From<IPublishRevit>().Build);
}