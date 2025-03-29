namespace MyProjectEntity
{
    public interface IHttpWrapper
    {
        Task<T> GetAsync<T>(string endpoint, params (string key, string value)[] queryParams);
        Task<T> GetByIdAsync<T>(string endpoint, int id, params (string key, string value)[] queryParams);
        Task<int> PostAsync<T>(string endpoint, T data);
        Task<bool> PutAsync<T>(string endpoint, T data);
        Task<bool> DeleteAsync(string endpoint);
    }
}
