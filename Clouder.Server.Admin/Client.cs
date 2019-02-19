using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Clouder.Server.Admin
{
    public class Client
    {
        private readonly HttpClient httpClient;

        public Client()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri($"{AppInfo.Instance.ApiBaseAddress}/")
            };
        }

        public async Task<Out> Call<Out>(string action, Dictionary<string, string> parameters)
        {
            try
            {
                var requestUri = $"{action}?";
                foreach (var parameter in parameters)
                {
                    requestUri += $"{parameter.Key}={parameter.Value}&";
                }

                var json = await httpClient.GetStringAsync(requestUri.TrimEnd(new[] { '&' }));
                return await Task.Run(() => JsonConvert.DeserializeObject<Out>(json));
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Out> Call<In, Out>(string action, In input)
        {
            try
            {
                var serializedInput = JsonConvert.SerializeObject(input);
                var response = await httpClient.PostAsync(action, new StringContent(serializedInput, Encoding.UTF8, "application/json"));
                var seralizedResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Out>(seralizedResult);
                return result;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        public async Task Call<In>(string action, In input)
        {
            try
            {
                var serializedInput = JsonConvert.SerializeObject(input);
                var response = await httpClient.PostAsync(action, new StringContent(serializedInput, Encoding.UTF8, "application/json"));
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}