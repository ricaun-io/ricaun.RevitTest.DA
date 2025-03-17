using ricaun.RevitTest.DA.Application.Extensions;
using System;
using System.IO;
using System.IO.Compression;

namespace ricaun.RevitTest.DA.Application.Models
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

        internal static void InputZip()
        {
            var inputName = "input";
            var outputName = "output.zip";

            var inputDirectory = Path.Combine(Directory.GetCurrentDirectory(), inputName);
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), outputName);
            Console.WriteLine($"InputDirectory: {inputDirectory}");
            Console.WriteLine($"OutputDirectory: {outputDirectory}");
            if (Directory.Exists(inputDirectory))
            {
                Console.WriteLine($"ZipFile: {outputName}");
                ZipFile.CreateFromDirectory(inputDirectory, outputDirectory, CompressionLevel.Optimal, false);
            }
        }

        internal static void OutputFolder()
        {
            var outputName = "output";
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), outputName);
            if (Directory.Exists(outputDirectory))
            {
                Console.WriteLine($"OutputFolder Exists: {Path.GetFileName(outputDirectory)}");
                return;
            }
            foreach (var directory in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                Console.WriteLine($"OutputFolder: {Path.GetFileName(directory)}");
                if (!Directory.Exists(outputDirectory))
                {
                    Console.WriteLine($"OutputFolder: {Path.GetFileName(directory)} to {outputName}");

                    DirectoryCopy(directory, outputDirectory);
                }
            };
        }

        private static void DirectoryCopy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));

            foreach (var directory in Directory.GetDirectories(sourceDir))
                DirectoryCopy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }
    }
}
