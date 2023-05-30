using ricaun.DA4R.NUnit.Extensions;
using System;
using System.IO;

namespace ricaun.DA4R.NUnit.Models
{
    public class OutputModelUtils
    {
        private const string JSON_FILE = "output.json";
        private const string ZIP_FILE = "output.zip";
        public static void ZipFolder()
        {
            try
            {
                ZipExtension.ZipCurrentFolder(ZIP_FILE, JSON_FILE);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ZipExtension: {ex}");
            }
        }
    }
}
