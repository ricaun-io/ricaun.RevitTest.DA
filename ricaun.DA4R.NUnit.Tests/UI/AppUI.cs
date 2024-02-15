using Autodesk.Revit.UI;

namespace ricaun.DA4R.NUnit.Tests.UI
{
    public static class AppUI
    {
        private class App
        {
            public Result OnStartup(UIControlledApplication application)
            {
                return Result.Succeeded;
            }

            public Result OnShutdown(UIControlledApplication application)
            {
                return Result.Succeeded;
            }
        }
    }
}
