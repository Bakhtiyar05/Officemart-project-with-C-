using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace OfficeMart.UI.API
{
    public static class NetworkManager
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<T> SendRequestAsync<T>(string url, IConfiguration configuration, HttpMethod method, string jsonBody = null)
        {
            try
            {
                string username = configuration.GetValue<string>("ApiInfos:username");
                string password = configuration.GetValue<string>("ApiInfos:password");
                string baseUrl = configuration.GetValue<string>("ApiInfos:baseUrl");
                var fullUrl = $"{baseUrl}{url}";

                var request = new HttpRequestMessage(method, fullUrl);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{username}:{password}"))
                );


                if (!string.IsNullOrEmpty(jsonBody))
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(responseContent);

                return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return default;
            }
        }
    }
}
