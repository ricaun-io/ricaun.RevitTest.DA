﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricaun.DA4R.NUnit.Extensions
{
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
            var zipDirectory = Path.GetDirectoryName(zipFile);
            zipDestination = Path.Combine(zipDirectory, zipName);

            if (!Directory.Exists(zipDestination))
                Directory.CreateDirectory(zipDestination);

            try
            {
                ZipFile.ExtractToDirectory(zipFile, zipDestination);
                return true;
            }
            catch { }

            return false;
        }
    }
}
