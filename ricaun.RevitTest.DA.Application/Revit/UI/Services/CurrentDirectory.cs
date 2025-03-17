using System;
using System.IO;
using System.Reflection;

namespace ricaun.RevitTest.DA.Application.Revit.UI.Services
{
    public class CurrentDirectory : IDisposable
    {
        private readonly string CurrentDirectoryLast;

        public CurrentDirectory()
        {
            CurrentDirectoryLast = Directory.GetCurrentDirectory();
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(directory);
        }

        public CurrentDirectory(string directory)
        {
            CurrentDirectoryLast = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(directory);
        }

        public void Dispose()
        {
            Directory.SetCurrentDirectory(CurrentDirectoryLast);
        }
    }
}
