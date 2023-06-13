using System;
using System.Collections.Generic;

namespace ricaun.DA4R.NUnit.Console
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

    //
    // Summary:
    //     TestAssemblyModel
    public class TestAssemblyModel : TestModel
    {
        //
        // Summary:
        //     FileName
        public string FileName { get; set; }
        //
        // Summary:
        //     Version
        public string Version { get; set; }
        //
        // Summary:
        //     SuccessHate
        public double SuccessHate { get; set; }
        //
        // Summary:
        //     Tests
        public List<TestTypeModel> Tests { get; set; }
        //
        // Summary:
        //     Test Count
        public int TestCount { get; set; }
    }

    //
    // Summary:
    //     TestTypeModel
    public class TestTypeModel : TestModel
    {
        //
        // Summary:
        //     Tests
        public List<TestModel> Tests { get; set; }
        //
        // Summary:
        //     Test Count
        public int TestCount { get; set; }
        //
        // Summary:
        //     SuccessHate
        public double SuccessHate { get; set; }
    }

    //
    // Summary:
    //     Test Model
    public class TestModel
    {
        //
        // Summary:
        //     Test Name
        public string Name { get; set; }
        //
        // Summary:
        //     Test Alias
        public string Alias { get; set; }
        //
        // Summary:
        //     Test FullName
        public string FullName { get; set; }
        //
        // Summary:
        //     Test Success?
        public bool Success { get; set; }
        //
        // Summary:
        //     Test Skipped
        public bool Skipped { get; set; }
        //
        // Summary:
        //     Message Output
        public string Message { get; set; }
        //
        // Summary:
        //     Console Output
        public string Console { get; set; }
        //
        // Summary:
        //     Time in milliseconds
        public double Time { get; set; }

        public override string ToString()
        {
            var result = Skipped ? "Skipped" : Success ? "Passed" : "Failed";
            var name = string.IsNullOrEmpty(Alias) ? Name : Alias != Name ? $"{Alias}[{Name}]" : Alias;
            var message = (Message + " " + Console).Trim();
            return $"{name}\t {result}\t {message}".Replace("\n", " ").Replace("\r", " ");
        }
    }
}