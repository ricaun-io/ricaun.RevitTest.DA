using System;
using System.IO;

namespace ricaun.DA4R.NUnit.Models
{
    public class OutputModelUtils
    {
        private const string JSON_FILE = "output.json";
        private const string ZIP_FILE = "output.zip";
        public static void ZipToJson()
        {
            if (File.Exists(ZIP_FILE))
            {
                Console.WriteLine("----------");
                Console.WriteLine($"Convert: {ZIP_FILE} to {JSON_FILE}");
                File.Delete(JSON_FILE);
                File.Move(ZIP_FILE, JSON_FILE);
                Console.WriteLine("----------");
            }
        }
    }
}
