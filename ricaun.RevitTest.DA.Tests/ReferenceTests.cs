//using Autodesk.Revit.ApplicationServices;
//using Autodesk.Revit.UI;
//using NUnit.Framework;
//using System;
//using System.Linq;

//namespace ricaun.RevitTest.DA.Tests
//{
//#if DEBUG
//    public class ReferenceTests
//    {
//        public static string[] GetTypes()
//        {
//            var types = new Type[] { typeof(UIApplication), typeof(Application) };
//            return types.Select(e => e.Assembly.ToString()).ToArray();
//        }

//        [Explicit]
//        [TestCaseSource(nameof(GetTypes))]
//        public void TestRevitReferences(string typeName)
//        {
//            Console.WriteLine(typeName);
//            Assert.True(true);
//        }
//    }
//#endif
//}