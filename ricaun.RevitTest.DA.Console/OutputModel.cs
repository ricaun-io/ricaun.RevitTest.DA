using ricaun.NUnit.Models;
using System;
using System.Collections.Generic;

namespace ricaun.RevitTest.DA.Console
{
    public class OutputModel
    {
        public string VersionName { get; set; }
        public string VersionBuild { get; set; }
        public DateTime TimeStart { get; set; } = DateTime.UtcNow;
        public DateTime TimeFinish { get; set; }
        public bool Success { get; set; }
        public List<TestAssemblyModel> Tests { get; set; } = new List<TestAssemblyModel>();
    }
}