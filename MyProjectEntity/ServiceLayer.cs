using Entities;
using Microsoft.CodeAnalysis.Host.Mef;
using Newtonsoft.Json;

namespace MyProjectEntity
{
    public class ServiceLayer<T> where T : class
    {
        private readonly IHttpWrapper _httpWrapper;
        private readonly string _apiBaseUrl;

        public ServiceLayer(IHttpWrapper httpWrapper, IConfiguration configuration)
        {
            _httpWrapper = httpWrapper;
            _apiBaseUrl = configuration["ApiBaseUrl"]; // Read from appsettings.json
        }

        public async Task<List<T>> GetAllAsync(string endpoint)
        {
            return await _httpWrapper.GetAsync<List<T>>($"{_apiBaseUrl}/{endpoint}");
        }

        public async Task<T> GetByIdAsync(string endpoint, int id)
        {
            return await _httpWrapper.GetByIdAsync<T>($"{_apiBaseUrl}/{endpoint}", id);
        }

        public async Task<int> AddAsync(string endpoint, T entity)
        {
            int response = await _httpWrapper.PostAsync($"{_apiBaseUrl}/{endpoint}", entity);
            return response ;
        }

        public async Task<bool> UpdateAsync(string endpoint, T entity)
        {
            return await _httpWrapper.PutAsync($"{_apiBaseUrl}/{endpoint}", entity);
        }

        public async Task<bool> DeleteAsync(string endpoint, int id)
        {
            return await _httpWrapper.DeleteAsync($"{_apiBaseUrl}/{endpoint}/{id}");
        }
    }


}
