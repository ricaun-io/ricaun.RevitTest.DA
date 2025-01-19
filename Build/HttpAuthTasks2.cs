using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

/// <summary>
/// Provides a set of methods for performing HTTP requests with authentication.
/// </summary>
public static class HttpAuthTasks2
{
    #region HttpPost
    /// <summary>
    /// Performs an asynchronous HTTP POST request with a file as the content.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="filePath">The path of the file to be sent as content.</param>
    /// <param name="authorization">The authorization token for the request.</param>
    /// <param name="formData">The form data to be included in the request.</param>
    /// <param name="fileStreamContentName">The name of the file stream content.</param>
    /// <param name="clientConfigurator">The configurator for the HttpClient.</param>
    /// <param name="headerConfigurator">The configurator for the HttpRequestHeaders.</param>
    /// <returns>The response content as a string.</returns>
    public static async Task<string> HttpPostFileAsync(
        string uri,
        string filePath,
        string authorization = null,
        Dictionary<string, string> formData = null,
        string fileStreamContentName = null,
        Configure<HttpClient> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        using var content = CreateFormDataContent(filePath, formData, fileStreamContentName);
        return await HttpPostAsync(uri, content, authorization, clientConfigurator, headerConfigurator);
    }

    /// <summary>
    /// Performs a synchronous HTTP POST request with a file as the content.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="filePath">The path of the file to be sent as content.</param>
    /// <param name="authorization">The authorization token for the request.</param>
    /// <param name="formData">The form data to be included in the request.</param>
    /// <param name="fileStreamContentName">The name of the file stream content.</param>
    /// <param name="clientConfigurator">The configurator for the HttpClient.</param>
    /// <param name="headerConfigurator">The configurator for the HttpRequestHeaders.</param>
    /// <returns>The response content as a string.</returns>
    public static string HttpPostFile(
        string uri,
        string filePath,
        string authorization = null,
        Dictionary<string, string> formData = null,
        string fileStreamContentName = null,
        Configure<HttpClient> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        return HttpPostFileAsync(uri, filePath, authorization, formData, fileStreamContentName, clientConfigurator, headerConfigurator).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Performs an asynchronous HTTP POST request with a JSON object as the content.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="content">The JSON object to be sent as content.</param>
    /// <param name="authorization">The authorization token for the request.</param>
    /// <param name="clientConfigurator">The configurator for the HttpClient.</param>
    /// <param name="headerConfigurator">The configurator for the HttpRequestHeaders.</param>
    /// <returns>The response content as a string.</returns>
    public static async Task<string> HttpPostAsync(
        string uri,
        object content,
        string authorization = null,
        Configure<HttpClient> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        return await HttpPostAsync(uri, JsonHttpContent(content), authorization, clientConfigurator, headerConfigurator);
    }

    /// <summary>
    /// Performs a synchronous HTTP POST request with a JSON object as the content.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="content">The JSON object to be sent as content.</param>
    /// <param name="authorization">The authorization token for the request.</param>
    /// <param name="clientConfigurator">The configurator for the HttpClient.</param>
    /// <param name="headerConfigurator">The configurator for the HttpRequestHeaders.</param>
    /// <returns>The response content as a string.</returns>
    public static string HttpPost(
        string uri,
        object content,
        string authorization = null,
        Configure<HttpClient> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        return HttpPostAsync(uri, content, authorization, clientConfigurator, headerConfigurator).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Performs an asynchronous HTTP POST request with a custom HttpContent object as the content.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="httpContent">The custom HttpContent object to be sent as content.</param>
    /// <param name="authorization">The authorization token for the request.</param>
    /// <param name="clientConfigurator">The configurator for the HttpClient.</param>
    /// <param name="headerConfigurator">The configurator for the HttpRequestHeaders.</param>
    /// <returns>The response content as a string.</returns>
    public static async Task<string> HttpPostAsync(
        string uri,
        HttpContent httpContent = null,
        string authorization = null,
        Configure<HttpClient> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        var httpClient = CreateHttpClient(authorization, clientConfigurator, headerConfigurator);
        return await httpClient.PostAsync(uri, httpContent).Result.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Performs a synchronous HTTP POST request with a custom HttpContent object as the content.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="httpContent">The custom HttpContent object to be sent as content.</param>
    /// <param name="authorization">The authorization token for the request.</param>
    /// <param name="clientConfigurator">The configurator for the HttpClient.</param>
    /// <param name="headerConfigurator">The configurator for the HttpRequestHeaders.</param>
    /// <returns>The response content as a string.</returns>
    public static string HttpPost(
        string uri,
        HttpContent httpContent = null,
        string authorization = null,
        Configure<HttpClient> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        return HttpPostAsync(uri, httpContent, authorization, clientConfigurator, headerConfigurator).GetAwaiter().GetResult();
    }
    #endregion

    #region HttpClient
    /// <summary>
    /// DefaultTimeout (30 seconds)
    /// </summary>
    public static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

    private static HttpClient CreateHttpClient(
        string authorization = null,
        Configure<HttpClient> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        var httpClient = new HttpClient { Timeout = DefaultTimeout };
        SetBearerAuthorization(httpClient, authorization);
        clientConfigurator?.Invoke(httpClient);
        headerConfigurator?.Invoke(httpClient.DefaultRequestHeaders);
        return httpClient;
    }
    private static void SetBearerAuthorization(HttpClient httpClient, string authorization = null)
    {
        const string HeaderAuthorization = "Authorization";
        if (!string.IsNullOrEmpty(authorization))
        {
            httpClient.DefaultRequestHeaders.Add(HeaderAuthorization, $"Bearer {authorization}");
        }
    }

    private static HttpContent JsonHttpContent(object content)
    {
        if (content is null)
            content = string.Empty;

        HttpContent httpContent = null;
        if (content is string stringContent)
        {
            httpContent = new StringContent(stringContent);
        }
        else if (content is HttpContent httpContentTo)
        {
            httpContent = httpContentTo;
        }
        else
        {
            const string MediaTypeJson = "application/json";
            httpContent = new StringContent(content.ToJson(), System.Text.Encoding.UTF8, MediaTypeJson);
        }

        return httpContent;
    }

    internal static MultipartFormDataContent CreateFormDataContent(string filePath, Dictionary<string, string> formData = null, string fileStreamContentName = null)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);

        var content = new MultipartFormDataContent();

        if (formData is not null)
        {
            foreach (KeyValuePair<string, string> vp in formData)
            {
                content.Add(new StringContent(vp.Value), vp.Key);
            }
        }

        if (string.IsNullOrEmpty(fileStreamContentName)) fileStreamContentName = "file";

        var streamContent = new StreamContent(new FileStream(filePath, FileMode.Open));
        content.Add(streamContent, fileStreamContentName, Path.GetFileName(filePath));

        return content;
    }
    #endregion

    #region Json
    internal static string ToJson(this object obj)
    {
        if (obj is string t)
            return t;

        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }
    internal static T FromJson<T>(this string json)
    {
        if (json is T t)
            return t;

        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }
    #endregion
}

