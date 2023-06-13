using System;
using System.IO;

namespace ricaun.DA4R.NUnit.Console.Utils
{
    public class ZipFileTemporary : IDisposable
    {
        public string ZipFilePath { get; }
        public ZipFileTemporary(string filePath)
        {
            ZipFilePath = ZipExtension.CreateFromFileToTemporaryDirectory(filePath);
        }

        public void Dispose()
        {
            try
            {
                if (File.Exists(ZipFilePath))
                {
                    File.Delete(ZipFilePath);
                }
            }
            catch { }
        }
    }
}