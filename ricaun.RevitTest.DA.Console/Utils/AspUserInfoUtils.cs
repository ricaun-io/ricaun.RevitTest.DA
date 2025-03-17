using Autodesk.Forge.Oss.DesignAutomation.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ricaun.RevitTest.DA.Console.Utils
{
    /// <summary>
    /// AspUserInfoUtils
    /// </summary>
    /// <remarks>
    /// https://aps.autodesk.com/en/docs/profile/v1/reference/profile/oidcuserinfo/
    /// </remarks>
    public class AspUserInfoUtils
    {
        /// <summary>
        /// UserInfoUrl
        /// </summary>
        /// <remarks>
        /// https://aps.autodesk.com/en/docs/profile/v1/reference/profile/oidcuserinfo/
        /// </remarks>
        public const string UserInfoUrl = "https://api.userprofile.autodesk.com/userinfo";

        /// <summary>
        /// Retrieves user information from Autodesk's user profile service.
        /// </summary>
        /// <param name="accessToken">The access token used for authorization.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user information.</returns>
        public static async Task<AspUserInfo> GetUserInfo(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(UserInfoUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content.FromJson<AspUserInfo>();
            }
        }
    }

    /// <summary>
    /// AspUserInfo
    /// </summary>
    /// <remarks>
    /// There are more information but we are only interested in know the user name.
    /// </remarks>
    public class AspUserInfo
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string sub { get; set; }
        /// <summary>
        /// Full User Name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// User Name
        /// </summary>
        public string preferred_username { get; set; }
        public override string ToString()
        {
            return preferred_username;
        }
    }
}
