using System;
using System.Collections.Generic;
using System.Linq;

namespace Autodesk.Revit.DB
{
    /// <summary>
    /// RevitParameters
    /// </summary>
    public static class RevitParameters
    {
        private static List<object> recordParameters = new List<object>();

        /// <summary>
        /// AddParameter
        /// </summary>
        /// <param name="parameters"></param>
        public static void AddParameter(params object[] parameters)
        {
            foreach (var parameter in parameters)
                recordParameters.Add(parameter);
        }

        /// <summary>
        /// Parameters
        /// </summary>
        public static object[] Parameters => GetParameters(recordParameters.ToArray());

        private static object[] GetParameters(params object[] parameters)
        {
            var list = new List<object>();
            list.AddRange(parameters);
            list.AddRange(RevitApplicationParameters.GetUIParameters());

            return list.ToArray();
        }
        private static class RevitApplicationParameters
        {
            public static object[] GetUIParameters()
            {
                if (HasRevitAPIUI()) return RevitApplicationUIParameters.GetUIParameters();
                return new object[] { };
            }

            private static bool HasRevitAPIUI()
            {
                return AppDomain.CurrentDomain.GetAssemblies().Any(e => e.GetName().Name.Equals("RevitAPIUI"));
            }

            private static class RevitApplicationUIParameters
            {
                public static object[] GetUIParameters()
                {
                    var list = new List<object>();
                    list.Add(Autodesk.Revit.UI.RevitApplication.UIApplication);
                    list.Add(Autodesk.Revit.UI.RevitApplication.UIControlledApplication);
                    return list.ToArray();
                }
            }
        }
    }
}
