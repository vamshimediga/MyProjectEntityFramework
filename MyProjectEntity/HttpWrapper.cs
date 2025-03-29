using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace MyProjectEntity
{
    public class HttpWrapper : IHttpWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string endpoint, params (string key, string value)[] queryParams)
        {
            string url = endpoint;
            if (queryParams.Length > 0)
            {
                url += "?" + string.Join("&", queryParams.Select(q => $"{q.key}={q.value}"));
            }

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<T> GetByIdAsync<T>(string endpoint, int id, params (string key, string value)[] queryParams)
        {
            string url = $"{endpoint}/{id}";
            if (queryParams.Length > 0)
            {
                url += "?" + string.Join("&", queryParams.Select(q => $"{q.key}={q.value}"));
            }

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<int> PostAsync<T>(string endpoint, T data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<int>(jsonResponse);
        }

        public async Task<bool> PutAsync<T>(string endpoint, T data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

       
    }

}
