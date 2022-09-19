using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricaun.DA4R.NUnit.Models
{
    public class OutputModel
    {
        public string VersionName { get; set; }
        public string VersionBuild { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public List<object> Tests { get; set; } = new List<object>();

        public OutputModel Load(string jsonPath = "output.json")
        {
            if (File.Exists(jsonPath))
            {
                string jsonContents = File.ReadAllText(jsonPath);
                return JsonConvert.DeserializeObject<OutputModel>(jsonContents);
            }
            return this;
        }

        public string Save(string jsonPath = "output.json")
        {
            string text = JsonConvert.SerializeObject(this);
            using (StreamWriter sw = File.CreateText(jsonPath))
            {
                sw.WriteLine(text);
                sw.Close();
            }
            return text;
        }
    }
}
