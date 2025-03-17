using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricaun.RevitTest.DA.Application.Extensions
{
    /// <summary>
    /// ZipExtension
    /// </summary>
    public static class ZipExtension
    {
        /// <summary>
        /// Extract <paramref name="zipFile"/> to Folder
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="zipDestination"></param>
        /// <returns></returns>
        public static bool ExtractToFolder(string zipFile, out string zipDestination)
        {
            var zipName = Path.GetFileNameWithoutExtension(zipFile);
#if DEBUG
            zipName = $"{zipName}_{DateTime.Now.Ticks}";
#endif
            var zipDirectory = Path.GetDirectoryName(zipFile);
            zipDestination = Path.Combine(zipDirectory, zipName);

            if (Directory.Exists(zipDestination))
            {
                try
                {
                    Directory.Delete(zipDestination, true);
                }
                catch { }
            }

            if (Directory.Exists(zipDestination))
                return true;

            Directory.CreateDirectory(zipDestination);

            try
            {
                ZipFile.ExtractToDirectory(zipFile, zipDestination);
                return true;
            }
            catch { }

            return false;
        }

        /// <summary>
        /// ZipCurrentFolder
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="excludedFiles"></param>
        public static void ZipCurrentFolder(string zipFileName, params string[] excludedFiles)
        {
            string sourceFolderPath = Directory.GetCurrentDirectory();
            string destinationZipPath = Path.Combine(sourceFolderPath, zipFileName);

            // Create a new ZIP file
            ZipFile.CreateFromDirectory(sourceFolderPath, destinationZipPath, CompressionLevel.Optimal, false);

            // Open the created ZIP file
            using (var zip = ZipFile.Open(destinationZipPath, ZipArchiveMode.Update))
            {
                // Iterate over the excluded files
                foreach (var excludedFile in excludedFiles)
                {
                    // Get the entry for the excluded file if it exists
                    var entry = zip.GetEntry(excludedFile);
                    if (entry != null)
                    {
                        // Remove the entry from the ZIP file
                        entry.Delete();
                    }
                }
            }
        }
    }
}
