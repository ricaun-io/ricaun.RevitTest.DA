using Newtonsoft.Json;
using ricaun.NUnit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ricaun.DA4R.NUnit.Models
{
    public class OutputModel
    {
        public string VersionName { get; set; }
        public string VersionBuild { get; set; }
        public DateTime TimeStart { get; set; } = DateTime.UtcNow;
        public DateTime TimeFinish { get; set; }
        public bool Success { get; set; }
        public List<TestAssemblyModel> Tests { get; set; } = new List<TestAssemblyModel>();

        #region JsonConvert
        private const string JSON_FILE = "output.json";
        public OutputModel Load(string jsonPath = JSON_FILE)
        {
            if (File.Exists(jsonPath))
            {
                string jsonContents = File.ReadAllText(jsonPath);
                return JsonConvert.DeserializeObject<OutputModel>(jsonContents);
            }
            return this;
        }

        public string Save(string jsonPath = JSON_FILE)
        {
            string text = JsonConvert.SerializeObject(this);
            using (StreamWriter sw = File.CreateText(jsonPath))
            {
                sw.WriteLine(text);
                sw.Close();
            }
            return text;
        }
        #endregion
    }
}
