using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ricaun.RevitTest.DA.Console.Utils
{
    public static class DownloadUtils
    {
        /// <summary>
        /// GetFileAsync
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<string> GetFileAsync(string requestUri, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = Path.GetFileName(requestUri);

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (HttpClient client = new HttpClient())
            {
                using (var s = await client.GetStreamAsync(requestUri))
                {
                    using (var fs = new FileStream(fileName, FileMode.CreateNew))
                    {
                        await s.CopyToAsync(fs);
                        return fs.Name;
                    }
                }
            }
        }
    }
}