using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Clouder.Server.Api.Extension
{
    public static class HttpRequestEx
    {
        public static string Parse(this HttpRequest req, string paramName)
        {
            string value = req.Query[paramName];
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"Parameter '{paramName}' was not provided.");
            }

            return value;
        }

        public static async Task<T> Parse<T>(this HttpRequest req)
        {
            string content = string.Empty;
            using (var reader = new StreamReader(req.Body))
            {
                content = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<T>(content);
        }

      
        public static async Task GetStream(this HttpRequest req, Stream targetStream)
        {
            using (var reader = new StreamReader(req.Body))
            {
                var buffer = new char[req.ContentLength.Value];
                var content = await reader.ReadAsync(buffer, 0, buffer.Length);
            }
        }

        public static T Parse<T>(this HttpRequest req, string headerName)
        {
            try
            {
                var unescaped = Uri.UnescapeDataString((req.Headers[headerName])).ToString();
                var meta = JsonConvert.DeserializeObject<T>(unescaped);
                return meta;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
    }
}
